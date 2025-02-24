namespace ArnoldiIteration
{
    public class ArnoldiResult
    {
        // Basis: the matrix with orthonormal columns representing the Krylov subspace.
        public double[, ]? Basis { get; set; }
        // Hessenberg: the upper Hessenberg matrix from the Arnoldi process.
        public double[, ]? Hessenberg { get; set; }

        public ArnoldiResult(double[, ]? basis, double[, ]? hessenberg)
        {
            Basis = basis;
            Hessenberg = hessenberg;
        }
    }
}