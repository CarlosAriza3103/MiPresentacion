using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MiPresentacion;

public partial class MainWindow : Window
{
    private const string MensajeInicial =
        "Completa el formulario y presiona «Presentarme».";

    // false = saludo 1, true = saludo 2 (reto adicional)
    private bool _saludoAlterno = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    // Valida los campos. Devuelve true si todo es correcto.
    // Si hay error, deja en "mensaje" el texto de advertencia.
    private bool TryObtenerDatos(out string nombre, out string apellido,
                                 out int edad, out string mensaje)
    {
        nombre = TxtNombre.Text?.Trim() ?? "";
        apellido = TxtApellido.Text?.Trim() ?? "";
        edad = 0;
        mensaje = "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            mensaje = "⚠ El nombre no puede estar vacío.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(apellido))
        {
            mensaje = "⚠ El apellido no puede estar vacío.";
            return false;
        }
        if (!int.TryParse(TxtEdad.Text, out edad))
        {
            mensaje = "⚠ La edad debe ser un número entero (solo dígitos).";
            return false;
        }
        if (edad <= 0)
        {
            mensaje = "⚠ La edad debe ser un número positivo mayor que cero.";
            return false;
        }
        return true;
    }

    private string ConstruirSaludo(string nombre, string apellido, int edad)
    {
        if (_saludoAlterno)
        {
            return $"¡Saludos! Soy {nombre} {apellido}, tengo {edad} años " +
                   "y me encanta crear aplicaciones de escritorio con Avalonia UI.";
        }
        return $"¡Hola! Mi nombre es {nombre} {apellido}, tengo {edad} años " +
               "y estoy aprendiendo a desarrollar interfaces gráficas con C#.";
    }

    private void BtnPresentarme_Click(object? sender, RoutedEventArgs e)
    {
        if (TryObtenerDatos(out var nombre, out var apellido, out var edad, out var msg))
            TxtResultado.Text = ConstruirSaludo(nombre, apellido, edad);
        else
            TxtResultado.Text = msg;
    }

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        TxtNombre.Text = "";
        TxtApellido.Text = "";
        TxtEdad.Text = "";
        TxtResultado.Text = MensajeInicial;
        TxtNombre.Focus();
    }

    // Reto adicional: alterna entre dos saludos sin reiniciar la app
    private void BtnCambiarSaludo_Click(object? sender, RoutedEventArgs e)
    {
        _saludoAlterno = !_saludoAlterno;

        if (TryObtenerDatos(out var nombre, out var apellido, out var edad, out _))
            TxtResultado.Text = ConstruirSaludo(nombre, apellido, edad);
        else
            TxtResultado.Text = _saludoAlterno
                ? "Saludo 2 activado. Presiona «Presentarme» para verlo."
                : "Saludo 1 activado. Presiona «Presentarme» para verlo.";
    }
}