namespace ProvaPub.Services
{
	public class RandomService
	{
		int seed;
		public RandomService()
		{
            // o problema é que o valor da seed era criado quando a controller criava a variável _randomService,
			// fazendo com que o valor dela não mudasse. Para ser criada um novo valor, é necessário que o GetHashCode()
			// seja invocado a cada vez que for requisitado a criação de um novo número aleatorio.
        }
        public int GetRandom()
		{
            seed = Guid.NewGuid().GetHashCode();

            return new Random(seed).Next(100);
		}

	}
}
