using System;
using System.Collections.Generic;

#nullable disable

namespace PolizaDB.Context
{
    public partial class Poliza
    {
        public int Id { get; set; }
        public int NumeroPoliza { get; set; }
        public string NombreCliente { get; set; }
        public string IdentificacionCliente { get; set; }
        public DateTime FechaNacimientoCliente { get; set; }
        public DateTime FechaPoliza { get; set; }
        public string CoberturaPoliza { get; set; }
        public decimal ValorPoliza { get; set; }
        public string NombrePlanPoliza { get; set; }
        public string CiudadCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string PlacaAutomotor { get; set; }
        public string ModeloAutomotor { get; set; }
        public bool? TieneInspeccion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
