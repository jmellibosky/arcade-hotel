using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BizHawk.Client.Common;

namespace BizHawkCoinTool
{
    [ExternalTool("Insert Coin Tool", Description = "Herramienta para insertar monedas en juegos de arcade.")]
    public class CoinInserter : Form, IExternalToolForm
    {
        private IJoypadApi _joypadApi;
        private TcpListener _server;
        private Thread _serverThread;

        public CoinInserter()
        {
            Text = "Insert Coin Tool"; // Nombre de la ventana
        }

        // Método para obtener las APIs de BizHawk
        public void SetApiProvider(IExternalApiProvider provider)
        {
            _joypadApi = (IJoypadApi)provider.GetApi(typeof(IJoypadApi));
        }

        public void Initialize()
        {
            // Inicia el servidor TCP en el puerto 5000
            _server = new TcpListener(IPAddress.Loopback, 5000);
            _server.Start();
            _serverThread = new Thread(ServerLoop) { IsBackground = true };
            _serverThread.Start();

            Console.WriteLine("Insert Coin Tool cargado y escuchando en el puerto 5000.");
        }

        private void ServerLoop()
        {
            while (true)
            {
                try
                {
                    using (var client = _server.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    {
                        byte[] buffer = new byte[256];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string comando = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                        if (comando.Equals("coin", StringComparison.OrdinalIgnoreCase))
                        {
                            InsertCoin();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en el servidor: " + ex.Message);
                }
            }
        }

        public void InsertCoin()
        {
            if (_joypadApi == null)
            {
                Console.WriteLine("Error: Joypad API no disponible.");
                return;
            }

            var inputs = new Dictionary<string, bool>
            {
                { "P1 Coin", true } // Ajusta según la configuración de controles en BizHawk
            };

            _joypadApi.Set(inputs, 1);
            Console.WriteLine("Moneda insertada.");
        }

        // Implementaciones requeridas por IExternalToolForm
        public bool IsActive => true;
        public bool IsLoaded => true;
        public void UpdateValues(ToolFormUpdateType type) { }
        public bool AskSaveChanges() => true;
        public void Restart() { }
        public void UpdateBefore() { }
        public void FastUpdate() { }
    }
}
