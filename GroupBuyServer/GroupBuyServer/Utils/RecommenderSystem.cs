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
        private static Dictionary<Guid, Dictionary<Guid, double>> m_matSimilarityMatrix = null;
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
            //double[,] matSimilarityMatrix = new double [p_svdMatrix.RowCount,p_svdMatrix.RowCount];
            m_matSimilarityMatrix = new Dictionary<Guid, Dictionary<Guid, double>>();

            for (int i = 0; i < p_svdMatrix.RowCount; i++)
			{
                Dictionary<Guid, double> currUserSimilarity = new Dictionary<Guid, double>();
			    for (int j = 0; j < p_svdMatrix.RowCount; j++)
			    {
                    if (i == j)
                    {
                        currUserSimilarity.Add(m_userList[j].Id, -1);
                    }
                    else
                    {
                        currUserSimilarity.Add(m_userList[j].Id, CalcCorrelation(p_svdMatrix.Row(i), p_svdMatrix.Row(j)));
                        //matSimilarityMatrix[i, j] = CalcCorrelation(p_svdMatrix.Row(i), p_svdMatrix.Row(j));
                    }
			    }
                m_matSimilarityMatrix.Add(m_userList[i].Id,currUserSimilarity);
			}
            //return matSimilarityMatrix;
        }

        private static double CalcCorrelation(IEnumerable<double> p_arrPurchasedProductsA, IEnumerable<double> p_arrPurchasedProductsB)
        {
            return Correlation.Pearson(p_arrPurchasedProductsA, p_arrPurchasedProductsB);
        }

        //public static double[][] getTruncatedSVD(Dictionary<Guid, Dictionary<Guid, int>> p_matCustomerProduct, int k) {
        //    double[,] matUserPurchasedProducts = new double[p_matCustomerProduct.Count, p_matCustomerProduct.First().Value.Count];
        //    int i = 0;
        //    int j;
        //    foreach (Dictionary<Guid, int> currUserPurchasedProducts in p_matCustomerProduct.Values)
        //    {
        //        j = 0;
        //        foreach (int currPurchasedProduct in currUserPurchasedProducts.Values)
        //        {
        //            matUserPurchasedProducts[i, j] = currPurchasedProduct;
        //            j++;
        //        }
        //        i++;
        //    }

        //    DenseMatrix m = DenseMatrix.OfArray(matUserPurchasedProducts);

        //    Svd<double> svd = m.Svd(true);

        //    double[,] truncatedU = new double[svd.U.RowCount,k];
        //    svd.U.copySubMatrix(0, truncatedU.Length - 1, 0, k - 1, truncatedU);
        //    Matrix<double> a;
        //    svd.U.CopyTo(a);
        //    a.Multiply
        //    double[][] truncatedS = new double[k][k];
        //    svd.getS().copySubMatrix(0, k - 1, 0, k - 1, truncatedS);

        //    double[][] truncatedVT = new double[k][svd.getVT().getColumnDimension()];
        //    svd.getVT().copySubMatrix(0, k - 1, 0, truncatedVT[0].length - 1, truncatedVT);

        //    RealMatrix approximatedSvdMatrix = (new Array2DRowRealMatrix(truncatedU)).multiply(new Array2DRowRealMatrix(truncatedS)).multiply(new Array2DRowRealMatrix(truncatedVT));

        //    return approximatedSvdMatrix.getData();
        //}

        //private void copySubMatrix(int startRow, int endRow, int startColumn, int endColumn, double[,] destination)
        //{
        //    for (int i = startRow; i <= endRow; i++)
        //    {
        //        for (int j = startColumn; j <= endColumn; j++)
        //        {
        //            destination[i-startRow,j-startColumn] = 
        //        }
        //    }
        //}
    }
}