using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.UI;
using System.Web.UI.WebControls;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace PruebaMP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            MercadoPagoConfig.AccessToken = "APP_USR-3559781808840376-072921-1506048af5c9b358dbfe8ed3bc3368bd-1921495539";
            if (!IsPostBack)
            {
                // Crea la preferencia de pago
                var request = new PreferenceRequest
                {
                    Items = new List<PreferenceItemRequest>
                                    {
                                        new PreferenceItemRequest
                                        {
                                            Title = "Producto de prueba", // Título del producto
                                            Quantity = 1, // Cantidad de productos
                                            CurrencyId = "ARS", // Moneda (ARS para pesos argentinos)
                                            UnitPrice = 100, // Precio unitario

                                        }
                                    },
                   
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success= "https://localhost:44372/Default.aspx",
                        //Success = "https://www.tusitio.com/success", // URL a la que se redirige en caso de éxito
                        Failure = "https://www.tusitio.com/failure", // URL a la que se redirige en caso de fallo
                        Pending = "https://www.tusitio.com/pending" // URL a la que se redirige en caso de pago pendiente
                    },
                    AutoReturn = "approved" // Retorno automático a la URL de éxito después del pago aprobado
                };

                // Crea un cliente de preferencia y guarda la preferencia
                var client = new PreferenceClient();
            Preference preference = client.Create(request);

            // Guarda el ID de la preferencia en ViewState para usarlo después
            Session["PreferenceId"] = preference.Id;
        }
    }

 

        protected void btnLinkPagoMP_Click(object sender, EventArgs e)
        {
            // Redirige al usuario a la URL de pago de Mercado Pago
            string preferenceId = Session["PreferenceId"].ToString();
            Response.Redirect($"https://www.mercadopago.com.ar/checkout/v1/redirect?pref_id={preferenceId}");
        }
    }
    }


