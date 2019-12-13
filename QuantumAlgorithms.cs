using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EigenVectorsAndValuesUsingDotNet
{
    public class QuantumAlgorithms
    {
        public static SquareRealMatrix Identity(int Dimension)
        {
            List<double> arrOfOnes = new List<double>();
            for (int i = 0; i < Dimension; i++)
            {
                arrOfOnes.Add((double)1);
            }

            return SquareRealMatrix.DiagonalMatrix(Dimension, arrOfOnes.ToArray());
        }

        public static SquareRealMatrix EigenVectorsFromEigenValues(SquareRealMatrix A, RealVector EValues)
        {
            SquareRealMatrix ret = new SquareRealMatrix(A.Rows, A.Columns);
            List<SquareRealMatrix> lstMs = new List<SquareRealMatrix>();
            List<SquareRealMatrix> lstMuls = new List<SquareRealMatrix>();
            SquareRealMatrix I = Identity(A.Rows);

            foreach (double ev in EValues)
            {
                lstMs.Add(A - ev * I);
            }

            for (int i = 0; i < lstMs.Count; i++)
            {
                for (int j = 0; j < lstMs.Count; j++)
                {
                    lstMuls.Add(lstMs[i] * lstMuls[j]);
                }
            }
            return ret;

        }
        public static void QRDecompostion(SquareRealMatrix A, out SquareRealMatrix Q, out SquareRealMatrix R)
        {
            Q = GramSchmidt(A);
            R = Q.Transpose() * A;
        }

        /*
        R program: https://stats.stackexchange.com/questions/20643/finding-matrix-eigenvectors-using-qr-decomposition
        Thanks so much!
        # some symmetric matrix
        A <- matrix( sample(1:30,16), ncol=4)
        A <- A + t(A);

        # initialize
        X <- A;
        pQ <- diag(1, dim(A)[1]);

        # iterate 
        for(i in 1:30)
        {
          d <- qr(X);
          Q <- qr.Q(d);
          pQ <- pQ %*% Q;
          X <- qr.R(d) %*% Q;
        }
        */

        public static SquareRealMatrix QRAlgorithmEigenVectors(SquareRealMatrix AIn, int Times)
        {
            SquareRealMatrix A = AIn;
            SquareRealMatrix Q = null;
            SquareRealMatrix R = null;

            SquareRealMatrix pQ = Identity(A.Rows);

            for (int i = 0; i < Times; i++)
            {
                QRDecompostion(A, out Q, out R);
                A = R * Q;
                pQ = pQ * Q;
            }

            return pQ;
        }
        public static SquareRealMatrix QRAlgorithm(SquareRealMatrix AIn, int Times)
        {
            SquareRealMatrix A = AIn;
            SquareRealMatrix Q = null;
            SquareRealMatrix R = null;

            for (int i = 0; i < Times; i++)
            {
                QRDecompostion(A, out Q, out R);

                A = R * Q;
            }
            return A;
        }

        /*
        Wikipedia : https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
        n = size(V,1);
            k = size(V,2);
            U = zeros(n,k);
            U(:,1) = V(:,1)/sqrt(V(:,1)'*V(:,1));
            for i = 2:k
              U(:,i) = V(:,i);
              for j = 1:i-1
                U(:,i) = U(:,i) - ( U(:,j)'*U(:,i) )/( U(:,j)'*U(:,j) )*U(:,j);
              end
              U(:,i) = U(:,i)/sqrt(U(:,i)'*U(:,i));
            end
        */
        public static SquareRealMatrix GramSchmidt(SquareRealMatrix V)
        {
            SquareRealMatrix U = new SquareRealMatrix(V.Rows, V.Columns);
            U[0] = Normalize(V[0]);

            for (int i = 1; i < V.Columns; i++)
            {
                U[i] = V[i];
                for (int j = 0; j <= i - 1; j++)
                {
                    U[i] = U[i] - (DotProduct(U[j], U[i]) / DotProduct(U[j], U[j])) * U[j];
                }

                U[i] = Normalize(U[i]);
            }
            return U;
        }
        public static RealVector Normalize(RealVector v)
        {
            return v / Math.Sqrt(DotProduct(v, v));
        }
        public static double DotProduct(RealVector v1, RealVector v2)
        {
            double ret = 0;

            if (v1.Count != v2.Count)
            {
                throw new Exception("Vectors must be equal in length");
            }

            for (int i = 0; i < v1.Count; i++)
            {
                ret += (v1[i] * v2[i]);
            }
            return ret;
        }
        /*
    % Given a real symmetric 3x3 matrix A, compute the eigenvalues
    % Note that acos and cos operate on angles in radians

    p1 = A(1,2)^2 + A(1,3)^2 + A(2,3)^2
    if (p1 == 0) 
       % A is diagonal.
       eig1 = A(1,1)
       eig2 = A(2,2)
       eig3 = A(3,3)
    else
       q = trace(A)/3               % trace(A) is the sum of all diagonal values
       p2 = (A(1,1) - q)^2 + (A(2,2) - q)^2 + (A(3,3) - q)^2 + 2 * p1
       p = sqrt(p2 / 6)
       B = (1 / p) * (A - q * I)    % I is the identity matrix
       r = det(B) / 2

       % In exact arithmetic for a symmetric matrix  -1 <= r <= 1
       % but computation error can leave it slightly outside this range.
       if (r <= -1) 
          phi = pi / 3
       elseif (r >= 1)
          phi = 0
       else
          phi = acos(r) / 3
       end

       % the eigenvalues satisfy eig3 <= eig2 <= eig1
       eig1 = q + 2 * p * cos(phi)
       eig3 = q + 2 * p * cos(phi + (2*pi/3))
       eig2 = 3 * q - eig1 - eig3     % since trace(A) = eig1 + eig2 + eig3
    end    
        */

        public static Real3EigenValues GetReal3EigenValues(SquareRealMatrix A)
        {
            Real3EigenValues ret = new Real3EigenValues();
            double q = 0;
            double p2 = 0;
            double p = 0;
            double p1 = 0;
            double phi = 0;
            double r = 0;
            SquareRealMatrix B = new SquareRealMatrix(A.Columns, A.Rows);

            SquareRealMatrix I = Identity(3);

            p1 = Math.Pow(A[0, 1], 2) + Math.Pow(A[0, 2], 2) + Math.Pow(A[1, 2], 2);

            if (p1 == 0) //diagonal matrix
            {
                ret.EigenValue1 = A[0, 0];
                ret.EigenValue2 = A[1, 1];
                ret.EigenValue3 = A[2, 2];
            }
            else
            {
                q = A.Trace() / 3;
                p2 = Math.Pow(A[0, 0] - q, 2) + Math.Pow(A[1, 1] - q, 2) + Math.Pow(A[2, 2] - q, 2) + 2 * p1;
                p = Math.Sqrt(p2 / 6);

                B = (1 / p) * (A - q * I);
                r = B.Determinant() / 2;

                if (r <= -1)
                    phi = Math.PI / 3;
                else if (r >= 1)
                    phi = 0;
                else
                    phi = Math.Acos(r) / 3;

                ret.EigenValue1 = q + 2 * p * Math.Cos(phi);
                ret.EigenValue3 = q + 2 * p * Math.Cos(phi + (2 * Math.PI / 3));
                ret.EigenValue2 = 3 * q - ret.EigenValue1 - ret.EigenValue3;     // since trace(A) = eig1 + eig2 + eig3


            }
            return ret;
        }


    }
}