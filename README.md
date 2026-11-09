# MiPresentacion

**Asignatura:** Electiva de Profundización I
**Grupo:** 4BR
**Docente:** Eduardo Pimienta León
**Estudiante:** Carlos David Ariza Ospino
**Taller:** Dibujando una interfaz más completa

---

## Descripción

MiPresentacion es una aplicación de escritorio desarrollada con **Avalonia UI 11** y **C#** (.NET). Permite registrar el nombre, el apellido y la edad de una persona mediante un formulario y, al presionar un botón, muestra una presentación personalizada.

La aplicación valida los datos antes de mostrar el mensaje y responde con advertencias cuando la información es incorrecta. Además, incluye el reto adicional: un botón que alterna entre dos mensajes de presentación sin cerrar ni reiniciar el programa.

## Estructura del proyecto

```
MiPresentacion/
│
├── Program.cs
├── App.axaml
├── App.axaml.cs
├── MainWindow.axaml
├── MainWindow.axaml.cs
├── MiPresentacion.csproj
├── .gitignore
├── README.md
└── capturas/
```

- `MainWindow.axaml`: estructura visual de la ventana (controles y distribución).
- `MainWindow.axaml.cs`: lógica en C# que responde a los eventos de los botones.

## Controles utilizados

| Control | Uso en la aplicación |
|---|---|
| `TextBlock` | Muestra el título «Mi primera aplicación gráfica» y el resultado (presentación o advertencia). |
| `TextBox` | Tres cajas de texto para ingresar nombre, apellido y edad. |
| `Button` | Presentarme, Limpiar y Cambiar saludo (reto adicional). |
| `StackPanel` | Organiza los controles de forma vertical y los botones de forma horizontal, usando `Margin` y `Spacing` para la distribución. |

Los controles que se utilizan desde C# se identifican con `x:Name`: `TxtNombre`, `TxtApellido`, `TxtEdad`, `BtnPresentarme`, `BtnLimpiar`, `BtnCambiarSaludo` y `TxtResultado`.

## Funcionamiento de los eventos

### Botón Presentarme (`BtnPresentarme_Click`)
1. Obtiene los datos escritos en los tres campos.
2. Comprueba que el nombre y el apellido no estén vacíos ni contengan solo espacios (`string.IsNullOrWhiteSpace`).
3. Verifica que la edad sea un número entero con `int.TryParse()` y que sea mayor que cero.
4. Si todo es correcto, muestra la presentación personalizada en `TxtResultado`.
5. Si algún dato es incorrecto, muestra un mensaje de advertencia indicando cuál es el problema.

### Botón Limpiar (`BtnLimpiar_Click`)
1. Vacía los tres campos del formulario.
2. Restaura el mensaje inicial en `TxtResultado`.
3. Coloca de nuevo el cursor en la caja del nombre con `Focus()`.

### Botón Cambiar saludo (`BtnCambiarSaludo_Click`) — reto adicional
Cambia el valor de una variable booleana que indica cuál de los dos saludos se usa. Si los datos del formulario son válidos, muestra de inmediato el saludo alterno; si no, informa qué saludo quedó activo. Todo ocurre sin cerrar ni reiniciar la aplicación.

## Casos de prueba

| Caso | Datos introducidos | Resultado esperado | Resultado obtenido |
|---|---|---|---|
| 1 | Nombre, apellido y edad válidos | Muestra la presentación | Correcto |
| 2 | Nombre vacío | Muestra una advertencia | Correcto |
| 3 | Apellido vacío | Muestra una advertencia | Correcto |
| 4 | Edad con letras | Solicita una edad numérica | Correcto |
| 5 | Edad negativa o igual a cero | Rechaza la edad | Correcto |
| 6 | Presionar Limpiar | Restablece el formulario | Correcto |
| Reto | Presionar Cambiar saludo | Alterna entre los dos mensajes | Correcto |



## Requisitos y ejecución

**Requisitos**
- .NET SDK 8 o superior
- Git (para clonar el repositorio)

**Pasos para ejecutar**

```bash
git clone https://github.com/CarlosAriza3103/MiPresentacion.git
cd MiPresentacion
dotnet run
```


## Pregunta de reflexión

**¿Cuál es la función de MainWindow.axaml y MainWindow.axaml.cs, y cómo se relacionan mediante x:Name y los eventos Click para permitir que el usuario interactúe con la aplicación?**

El archivo `MainWindow.axaml` es donde diseño cómo se ve la ventana: ahí pongo el título, las cajas de texto, los botones y el texto del resultado, y los acomodo con `StackPanel`, `Margin` y `Spacing`. Ese archivo solo describe la parte visual, no hace nada por sí solo.

El archivo `MainWindow.axaml.cs` es donde escribo qué debe pasar cuando el usuario hace algo. Allí están los métodos que leen lo que se escribió, validan los datos y deciden qué mensaje mostrar.

Para que los dos archivos se comuniquen se usan dos cosas. La primera es `x:Name`: al ponerle un nombre a un control en el XAML, por ejemplo `TxtNombre`, desde C# puedo usarlo directamente, como en `TxtNombre.Text`, para leer o cambiar su contenido. La segunda es el evento `Click`: en el XAML, el botón tiene `Click="BtnPresentarme_Click"`, y eso le indica a Avalonia que, cuando el usuario haga clic, debe ejecutar el método con ese nombre en C#.

Ambos archivos son partes de la misma clase `MainWindow` (es una clase `partial`), y por eso el código puede ver los controles que definí en el XAML.

Un ejemplo de mi aplicación: el usuario escribe su nombre, apellido y edad y presiona **Presentarme**. Avalonia ejecuta `BtnPresentarme_Click`, que lee `TxtNombre.Text`, `TxtApellido.Text` y `TxtEdad.Text`, valida los datos y escribe el resultado en `TxtResultado.Text`. Así, el XAML pone los controles y el C# les da el comportamiento.
