namespace cp2.Services
{
    public class ScoreService
    {
        // Score de 0 a 1000 baseado na idade e valor solicitado
        public int CalcularScore(DateTime dataNascimento, decimal valorSolicitado)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            int score = 500;

            if (idade >= 30) score += 100;
            if (idade >= 50) score += 100;
            if (valorSolicitado <= 5000) score += 200;
            else if (valorSolicitado <= 20000) score += 100;
            else score -= 200;

            return Math.Clamp(score, 0, 1000);
        }

        public bool Aprovado(int score) => score >= 500;
    }
}