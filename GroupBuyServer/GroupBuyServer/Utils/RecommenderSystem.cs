using GroupBuyServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.LinearAlgebra;

namespace GroupBuyServer.Utils
{
    public class RecommenderSystem
    {
        private static Dictionary<Guid, Dictionary<Guid, int>> m_matCustomerProduct = null;
        private static Dictionary<Guid, List<Guid>> m_matCustomerNeighborhood = null;
        private static User[] m_userList = null;

        public static void Init()
        {
            CreateCustomerPurchaseMatrix();
            Matrix<double> svdMatix = CreateSVDMatrix(m_matCustomerProduct);
            CreateSimilarityMatrix(svdMatix);
        }

        private static void CreateCustomerPurchaseMatrix()
        {
            m_matCustomerProduct = new Dictionary<Guid, Dictionary<Guid, int>>();
            using (var session = NHibernateHandler.CurrSession)
            {
                m_userList = session.Query<User>().ToArray();
                List<Product> lstProducts = session.Query<Product>().ToList();

                foreach (User currUser in m_userList)
                {
                    Dictionary<Guid, int> dicPurchasedProducts = new Dictionary<Guid, int>();
                    foreach (Product currProduct in lstProducts)
                    {
                        dicPurchasedProducts.Add(currProduct.Id, Convert.ToInt32(currProduct.Buyers.Contains(currUser)));
                    }
                    m_matCustomerProduct.Add(currUser.Id, dicPurchasedProducts);
                }
            }
        }

        private static Matrix<double> CreateSVDMatrix(Dictionary<Guid, Dictionary<Guid, int>> p_matCustomerProduct)
        {
            double[,] matUserPurchasedProducts = new double[p_matCustomerProduct.Count, p_matCustomerProduct.First().Value.Count];
            int i = 0;
            int j;
            foreach (Dictionary<Guid, int> currUserPurchasedProducts in p_matCustomerProduct.Values)
            {
                j = 0;
                foreach (int currPurchasedProduct in currUserPurchasedProducts.Values)
                {
                    matUserPurchasedProducts[i, j] = currPurchasedProduct;
                    j++;
                }
                i++;
            }

            DenseMatrix m = DenseMatrix.OfArray(matUserPurchasedProducts);

            Svd<double> svd = m.Svd(true);
            Matrix<double> svdMatix = svd.U * svd.W * svd.VT;

            return svdMatix;
        }

        private static void CreateSimilarityMatrix(Matrix<double> p_svdMatrix)
        {
            m_matCustomerNeighborhood = new Dictionary<Guid, List<Guid>>();

            for (int i = 0; i < p_svdMatrix.RowCount; i++)
			{
                Dictionary<Guid, double> currUserSimilarity = new Dictionary<Guid, double>();
			    for (int j = 0; j < p_svdMatrix.RowCount; j++)
			    {
                    if (i != j)
                    {
                        currUserSimilarity.Add(m_userList[j].Id, CalcCorrelation(p_svdMatrix.Row(i), p_svdMatrix.Row(j)));
                    }
			    }
                m_matCustomerNeighborhood.Add(m_userList[i].Id, currUserSimilarity.OrderByDescending(x => x.Value).Take(20).Select(x => x.Key).ToList());
			}
        }

        private static double CalcCorrelation(IEnumerable<double> p_arrPurchasedProductsA, IEnumerable<double> p_arrPurchasedProductsB)
        {
            return Correlation.Pearson(p_arrPurchasedProductsA, p_arrPurchasedProductsB);
        }

        public static List<User> GetUserIdsInNeighborhood(Guid p_userId,IList<User> p_users){
            List<User> result = new List<User>();
            foreach (User currUser in p_users){
                if(m_matCustomerNeighborhood[p_userId].Contains(currUser.Id)){
                    result.Add(currUser);
                }
            }
            return result;
        }
    }
}