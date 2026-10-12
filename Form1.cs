using System.Text.RegularExpressions;

namespace formularioc_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tbNombre.TextChanged += ValidarNombre;
            tbApellidos.TextChanged += ValidarApellido;
            tbEdad.TextChanged += ValidarEdad;
            tbEstatura.TextChanged += ValidarEstatura;
            tbTelefono.Leave += ValidarTelefono;
        }

        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;

            if (!TelefonoValido(textboxLocal.Text))
            {
                MessageBox.Show(
                    "Escriba un teléfono válido",
                    "Error Teléfono",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textboxLocal.BackColor = Color.IndianRed;
            }
            else
            {
                textboxLocal.BackColor = Color.LightGreen;
            }
        }

        private bool TelefonoValido(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado)
                   && valor.Length == 10;
        }

        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;

            if (!EsDecimalValido(textboxLocal.Text))
            {
                MessageBox.Show(
                    "Escriba una estatura válida",
                    "Error Estatura",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textboxLocal.Clear();
            }
        }

        private bool EsDecimalValido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);
        }

        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;

            if (!EsEnteroValido(textboxLocal.Text))
            {
                MessageBox.Show(
                    "Escriba una edad válida",
                    "Error Edad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textboxLocal.Clear();
            }
        }

        private bool EsEnteroValido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }

        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;

            if (!EsTextoValido(textboxLocal.Text))
            {
                MessageBox.Show(
                    "Escriba un nombre válido",
                    "Error Nombre",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textboxLocal.Clear();
            }
        }

        private void ValidarApellido(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;

            if (!EsTextoValido(textboxLocal.Text))
            {
                MessageBox.Show(
                    "Escriba un apellido válido",
                    "Error Apellido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textboxLocal.Clear();
            }
        }

        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = tbNombre.Text;
            string apellidos = tbApellidos.Text;
            string tel = tbTelefono.Text;
            string edad = tbEdad.Text;
            string estatura = tbEstatura.Text;
            string genero = "";

            if (rbFemenino.Checked)
            {
                genero = "Femenino";
            }
            else if (rbMasculino.Checked)
            {
                genero = "Masculino";
            }
            else if (rbOtro.Checked)
            {
                genero = "Otros";
            }

            if (string.IsNullOrWhiteSpace(nombres) ||
                string.IsNullOrWhiteSpace(apellidos) ||
                !EsEnteroValido(edad) ||
                !EsDecimalValido(estatura) ||
                !TelefonoValido(tel) ||
                string.IsNullOrEmpty(genero))
            {
                MessageBox.Show(
                    "Complete todos los campos correctamente.",
                    "Error de validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string mensaje =
                $"Nombre: {nombres}\n" +
                $"Apellidos: {apellidos}\n" +
                $"Teléfono: {tel}\n" +
                $"Edad: {edad}\n" +
                $"Estatura: {estatura}\n" +
                $"Género: {genero}";

            string rutaArchivo = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "RegistroFormulario.txt");

            try
            {
                using (StreamWriter escritor =
                       new StreamWriter(rutaArchivo, true))
                {
                    escritor.WriteLine(mensaje);
                    escritor.WriteLine();
                }

                MessageBox.Show(
                    mensaje + "\n\nDatos guardados correctamente.",
                    "Confirmación de Registro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron guardar los datos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            tbNombre.Clear();
            tbApellidos.Clear();
            tbEdad.Clear();
            tbEstatura.Clear();
            tbTelefono.Clear();

            rbFemenino.Checked = false;
            rbMasculino.Checked = false;
            rbOtro.Checked = false;
        }
    }
}
