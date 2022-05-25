using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using PolizaDB.DTOs;

#nullable disable

namespace PolizaDB.Context
{
    public partial class AutosContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AutosContext()
        {
        }

        public AutosContext(DbContextOptions<AutosContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Poliza> Polizas { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Year> Years { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=CESARTG; database=Autos;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.ToTable("Ciudad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.ToTable("Poliza");

                entity.Property(e => e.CiudadCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoberturaPoliza)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionCliente)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaNacimientoCliente).HasColumnType("date");

                entity.Property(e => e.FechaPoliza).HasColumnType("date");

                entity.Property(e => e.IdentificacionCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModeloAutomotor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.NombrePlanPoliza)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlacaAutomotor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValorPoliza).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Year>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Year");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public Response GuardarPoliza(PolizaDto poliza)
        {
            string descripcionRegistro = "";
            Response respuesta = new Response();
            try
            {
                DataTable dtDatos = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("[Sp_GuardarPoliza]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@NumeroPoliza", SqlDbType.Int).Value = poliza.NumeroPoliza;
                        cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = poliza.NombreCliente;
                        cmd.Parameters.Add("@IdentificacionCliente", SqlDbType.VarChar).Value = poliza.IdentificacionCliente;
                        cmd.Parameters.Add("@FechaNacimientoCliente", SqlDbType.DateTime).Value = poliza.FechaNacimientoCliente;
                        cmd.Parameters.Add("@FechaPoliza", SqlDbType.DateTime).Value = poliza.FechaPoliza;
                        cmd.Parameters.Add("@CoberturaPoliza", SqlDbType.VarChar).Value = poliza.CoberturaPoliza;
                        cmd.Parameters.Add("@ValorPoliza", SqlDbType.Decimal).Value = poliza.ValorPoliza;
                        cmd.Parameters.Add("@NombrePlanPoliza", SqlDbType.VarChar).Value = poliza.NombrePlanPoliza;
                        cmd.Parameters.Add("@CiudadCliente", SqlDbType.VarChar).Value = poliza.CiudadCliente;
                        cmd.Parameters.Add("@DireccionCliente", SqlDbType.VarChar).Value = poliza.DireccionCliente;
                        cmd.Parameters.Add("@PlacaAutomotor", SqlDbType.VarChar).Value = poliza.PlacaAutomotor;
                        cmd.Parameters.Add("@ModeloAutomotor", SqlDbType.VarChar).Value = poliza.ModeloAutomotor;
                        cmd.Parameters.Add("@TieneInspeccion", SqlDbType.Bit).Value = poliza.TieneInspeccion;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtDatos);


                        dtDatos.AsEnumerable().ToList().ForEach(item =>
                        {
                            descripcionRegistro = Convert.ToString(item["IdPoliza"]);
                        });



                        if (descripcionRegistro == "TIENE POLIZA PENDIENTE")
                        {
                            respuesta.Mensaje = descripcionRegistro;
                        }
                        else
                        {
                            respuesta.Mensaje = "Registrado";
                        }
                        respuesta.Data = descripcionRegistro;
                        respuesta.EsError = false;
                        return respuesta;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Ha ocurrido un error, por favor comuniquese con el area de sistemas.";
                respuesta.EsError = true;
                return respuesta;
            }
        }

        public Response ObtenerPoliza(int numeroPoliza, string placa)
        {
            Response respuesta = new Response();
            List<PolizaDto> poliza = new List<PolizaDto>();
            try
            {
                DataTable dtDatos = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("[Sp_ConsultarPoliza]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (numeroPoliza != 0)
                        {
                            cmd.Parameters.Add("@NumeroPoliza", SqlDbType.Int).Value = numeroPoliza;
                        }

                        if (placa != null)
                        {
                            cmd.Parameters.Add("@PlacaAutomotor", SqlDbType.VarChar).Value = placa;
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtDatos);


                        dtDatos.AsEnumerable().ToList().ForEach(item =>
                        {
                            poliza.Add(new PolizaDto()
                            {
                                Id = Convert.ToInt32(item["Id"]),
                                NumeroPoliza = Convert.ToInt32(item["NumeroPoliza"]),
                                NombreCliente = Convert.ToString(item["NombreCliente"]),
                                IdentificacionCliente = Convert.ToString(item["IdentificacionCliente"]),
                                FechaNacimientoCliente = Convert.ToDateTime(item["FechaNacimientoCliente"]),
                                FechaPoliza = Convert.ToDateTime(item["FechaPoliza"]),
                                CoberturaPoliza = Convert.ToString(item["CoberturaPoliza"]),
                                ValorPoliza = Convert.ToDecimal(item["ValorPoliza"]),
                                NombrePlanPoliza = Convert.ToString(item["NombrePlanPoliza"]),
                                CiudadCliente = Convert.ToString(item["CiudadCliente"]),
                                DireccionCliente = Convert.ToString(item["DireccionCliente"]),
                                PlacaAutomotor = Convert.ToString(item["PlacaAutomotor"]),
                                ModeloAutomotor = Convert.ToString(item["ModeloAutomotor"]),
                                TieneInspeccion = Convert.ToBoolean(item["TieneInspeccion"])

                            });

                        });

                        if (poliza.Count > 0)
                        {
                            respuesta.Mensaje = "Registro encontrado";
                        }
                        else
                        {
                            respuesta.Mensaje = "Registro no encontrado";
                        }

                        respuesta.Data = poliza;
                        respuesta.EsError = false;
                        return respuesta;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Ha ocurrido un error, por favor comuniquese con el area de sistemas.";
                respuesta.EsError = true;
                return respuesta;
            }
        }

        public Response ObtenerTodasPolizasFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            Response respuesta = new Response();
            List<PolizaDto> poliza = new List<PolizaDto>();
            try
            {
                DataTable dtDatos = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("[Sp_ConsultarPoliza]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = "FECHAS";
                        cmd.Parameters.Add("@fechaInicial", SqlDbType.Date).Value = fechaInicial;
                        cmd.Parameters.Add("@fechaFinal", SqlDbType.Date).Value = fechaFinal;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtDatos);


                        dtDatos.AsEnumerable().ToList().ForEach(item =>
                        {
                            poliza.Add(new PolizaDto()
                            {
                                Id = Convert.ToInt32(item["Id"]),
                                NumeroPoliza = Convert.ToInt32(item["NumeroPoliza"]),
                                NombreCliente = Convert.ToString(item["NombreCliente"]),
                                IdentificacionCliente = Convert.ToString(item["IdentificacionCliente"]),
                                FechaNacimientoCliente = Convert.ToDateTime(item["FechaNacimientoCliente"]),
                                FechaPoliza = Convert.ToDateTime(item["FechaPoliza"]),
                                CoberturaPoliza = Convert.ToString(item["CoberturaPoliza"]),
                                ValorPoliza = Convert.ToDecimal(item["ValorPoliza"]),
                                NombrePlanPoliza = Convert.ToString(item["NombrePlanPoliza"]),
                                CiudadCliente = Convert.ToString(item["CiudadCliente"]),
                                DireccionCliente = Convert.ToString(item["DireccionCliente"]),
                                PlacaAutomotor = Convert.ToString(item["PlacaAutomotor"]),
                                ModeloAutomotor = Convert.ToString(item["ModeloAutomotor"]),
                                TieneInspeccion = Convert.ToBoolean(item["TieneInspeccion"])

                            });

                        });

                        if (poliza.Count > 0)
                        {
                            respuesta.Mensaje = "Registros encontrados";
                        }
                        else
                        {
                            respuesta.Mensaje = "Registros no encontrados";
                        }

                        respuesta.Data = poliza;
                        respuesta.EsError = false;
                        return respuesta;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Ha ocurrido un error, por favor comuniquese con el area de sistemas.";
                respuesta.EsError = true;
                return respuesta;
            }
        }

        public Response Login(UserDto login)
        {
            Response respuesta = new Response();
            try
            {
                var usuarioLogin = Users.Where(c => c.UserName == login.UserName).FirstOrDefault();

                if (usuarioLogin == null)
                {
                    respuesta.Mensaje = "Usuario no encontrado";
                }
                else
                {
                    if (usuarioLogin.Password == login.Password)
                    {
                        respuesta.Mensaje = "Bienvenido al sistema";
                    }
                    else
                    {
                        respuesta.Mensaje = "Credenciales invalidas";
                    }
                }

                respuesta.EsError = false;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = ex.Message;
                return respuesta;
            }
        }
    }
}
