namespace ProvaPub.Models.Base {
    public class BaseModel {
        // Criado por causa do 4° requisito da parte2Controller para dizer ao repositório genérico
        // que aceite somente as models que implementem de BaseModel (Customer e Product) e mapeie-os.
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
