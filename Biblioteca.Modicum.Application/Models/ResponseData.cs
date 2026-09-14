namespace Biblioteca.Modicum.Application.Models
{
    public class ResponseData<T>
    {
        public bool Exitoso { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public T? Resultado { get; set; }
        public object Errores { get; set; } = string.Empty;
        private int Code { get; set; }
        public void SetCode(int code) => Code = code;
        public int GetCode() => Code;
    }
}
