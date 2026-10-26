namespace LogHub.Processing
{
    // Результат прогона вместо печати «Записано: X, пропущено: Y» прямо
    // из бизнес-логики, как было в god-классе. Теперь счётчики возвращаются,
    // и их можно проверить в тесте.
    public readonly struct ProcessingResult
    {
        public int Written { get; }
        public int Skipped { get; }

        public ProcessingResult(int written, int skipped)
        {
            Written = written;
            Skipped = skipped;
        }
    }
}
