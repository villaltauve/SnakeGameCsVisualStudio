using System.Drawing; // Importar el espacio de nombres para trabajar con gráficos y puntos.
using static System.Net.Mime.MediaTypeNames; // Importar el espacio de nombres estático para tipos MIME.
namespace WinFormsApp3 // Definir el espacio de nombres del proyecto.
{
    public partial class Form1 : Form // Definir la clase Form1 que hereda de Form.
    {
        enum Posicion // Definir una enumeración para las direcciones del gusano.
        {
            izquierda, derecha, arriba, abajo // Direcciones posibles del gusano.
        }

        private List<Point> snake; // Lista de puntos que representan el cuerpo del gusano.
        private Point food; // Punto que representa la comida.
        private Posicion objposicion; // Dirección actual del gusano.
        private int gridSize = 10; // Tamaño de la cuadrícula.
        private List<Point> particles; // Lista de puntos que representan las partículas.
        private System.Windows.Forms.Timer particleTimer; // Temporizador para las partículas.

        public Form1() // Constructor de la clase Form1.
        {
            InitializeComponent(); // Inicializar los componentes del formulario.
            this.StartPosition = FormStartPosition.CenterScreen; // Centrar la ventana.
            this.MaximizeBox = false; // Deshabilitar la maximización.
            snake = new List<Point> { new Point(50, 50) }; // Inicializar el gusano con un segmento en la posición (50, 50).
            objposicion = Posicion.abajo; // Establecer la dirección inicial del gusano hacia abajo.
            particles = new List<Point>(); // Inicializar la lista de partículas.
            particleTimer = new System.Windows.Forms.Timer(); // Inicializar el temporizador de partículas.
            particleTimer.Interval = 50; // Establecer el intervalo del temporizador.
            particleTimer.Tick += ParticleTimer_Tick; // Asignar el evento Tick del temporizador.
            GenerateFood(); // Generar la comida en una posición aleatoria.
        }

        private void timer1_Tick(object sender, EventArgs e) // Evento que se ejecuta en cada tick del temporizador.
        {
            MoveSnake(); // Mover el gusano.
            CheckCollision(); // Verificar colisiones.
            Invalidate(); // Invalidar el formulario para que se vuelva a dibujar.
        }

        private void MoveSnake() // Método para mover el gusano.
        {
            Point head = snake[0]; // Obtiene la cabeza del gusano.
            Point newHead = head; // Crea una nueva cabeza basada en la posición actual.

            if (objposicion == Posicion.derecha) // Si la dirección es derecha.
                newHead.X += gridSize; // Mueve la cabeza a la derecha.
            else if (objposicion == Posicion.izquierda) // Si la dirección es izquierda.
                newHead.X -= gridSize; // Mueve la cabeza a la izquierda.
            else if (objposicion == Posicion.arriba) // Si la dirección es arriba.
                newHead.Y -= gridSize; // Mueve la cabeza hacia arriba.
            else if (objposicion == Posicion.abajo) // Si la dirección es abajo.
                newHead.Y += gridSize; // Mueve la cabeza hacia abajo.

            snake.Insert(0, newHead); // Insertar la nueva cabeza al inicio de la lista.

            if (newHead == food) // Si la nueva cabeza está en la posición de la comida.
            {
                GenerateFood(); // Generar nueva comida.
                GenerateParticles(newHead); // Generar partículas en la posición de la comida.
                particleTimer.Start(); // Iniciar el temporizador de partículas.
            }
            else
            {
                snake.RemoveAt(snake.Count - 1); // Eliminar la cola del gusano.
            }
        }

        private void CheckCollision() // Método para verificar colisiones.
        {
            Point head = snake[0]; // Obtiene la cabeza del gusano.

            // Verificar colisión con los bordes.
            if (head.X < 0 || head.X >= ClientSize.Width || head.Y < 0 || head.Y >= ClientSize.Height)
            {
                GameOver(); // Termina el juego si hay colisión con los bordes.
            }

            // Verificar colisión con el cuerpo del gusano.
            for (int i = 1; i < snake.Count; i++)
            {
                if (head == snake[i])
                {
                    GameOver(); // Termina el juego si hay colisión con el cuerpo.
                }
            }
        }

        private void GenerateFood() // Método para generar comida en una posición aleatoria.
        {
            Random rand = new Random(); // Crea una instancia de Random.
            int maxX = ClientSize.Width / gridSize; // Calcula el número máximo de celdas en el eje X.
            int maxY = ClientSize.Height / gridSize; // Calcula el número máximo de celdas en el eje Y.
            food = new Point(rand.Next(maxX) * gridSize, rand.Next(maxY) * gridSize); // Genera una posición aleatoria para la comida.
        }

        private void GenerateParticles(Point position) // Método para generar partículas en una posición.
        {
            particles.Clear(); // Limpiar la lista de partículas.
            Random rand = new Random(); // Crea una instancia de Random.
            for (int i = 0; i < 20; i++) // Generar 20 partículas.
            {
                int offsetX = rand.Next(-gridSize, gridSize); // Generar un desplazamiento aleatorio en X.
                int offsetY = rand.Next(-gridSize, gridSize); // Generar un desplazamiento aleatorio en Y.
                particles.Add(new Point(position.X + offsetX, position.Y + offsetY)); // Añadir la partícula a la lista.
            }
        }

        private void ParticleTimer_Tick(object sender, EventArgs e) // Evento que se ejecuta en cada tick del temporizador de partículas.
        {
            particles.Clear(); // Limpiar la lista de partículas.
            particleTimer.Stop(); // Detener el temporizador de partículas.
        }

        private void GameOver() // Método para terminar el juego.
        {
            timer1.Stop(); // Detiene el temporizador.
            DialogResult result = MessageBox.Show("Perdiste, que fuerte\n¿Quieres intentar de nuevo?", "Fin", MessageBoxButtons.YesNo, MessageBoxIcon.Information); // Muestra un mensaje de fin de juego con opciones.
            if (result == DialogResult.Yes) // Si el usuario elige "Try Again".
            {
                RestartGame(); // Reiniciar el juego.
            }
            else // Si el usuario elige "Exit".
            {
                System.Windows.Forms.Application.Exit(); // Cierra la aplicación.
            }
        }

        private void RestartGame() // Método para reiniciar el juego.
        {
            snake.Clear(); // Limpiar la lista del gusano.
            snake.Add(new Point(50, 50)); // Inicializar el gusano con un segmento en la posición (50, 50).
            objposicion = Posicion.abajo; // Establecer la dirección inicial del gusano hacia abajo.
            GenerateFood(); // Generar la comida en una posición aleatoria.
            timer1.Start(); // Iniciar el temporizador.
        }

        private void Form1_Paint(object sender, PaintEventArgs e) // Evento que se ejecuta al repintar el formulario.
        {
            // Dibujar el gusano.
            foreach (Point p in snake)
            {
                e.Graphics.FillRectangle(Brushes.Purple, new Rectangle(p.X, p.Y, gridSize, gridSize)); // Dibuja cada segmento del gusano.
            }

            // Dibujar la comida.
            e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(food.X, food.Y, gridSize, gridSize)); // Dibuja la comida.

            // Dibujar las partículas.
            foreach (Point p in particles)
            {
                e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(p.X, p.Y, gridSize / 2, gridSize / 2)); // Dibuja cada partícula.
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // Evento que se ejecuta al presionar una tecla.
        {
            if (e.KeyCode == Keys.Left && objposicion != Posicion.derecha) // Si se presiona la tecla izquierda y la dirección no es derecha.
            {
                objposicion = Posicion.izquierda; // Cambia la dirección a izquierda.
            }
            else if (e.KeyCode == Keys.Right && objposicion != Posicion.izquierda) // Si se presiona la tecla derecha y la dirección no es izquierda.
            {
                objposicion = Posicion.derecha; // Cambia la dirección a derecha.
            }
            else if (e.KeyCode == Keys.Up && objposicion != Posicion.abajo) // Si se presiona la tecla arriba y la dirección no es abajo.
            {
                objposicion = Posicion.arriba; // Cambia la dirección a arriba.
            }
            else if (e.KeyCode == Keys.Down && objposicion != Posicion.arriba) // Si se presiona la tecla abajo y la dirección no es arriba.
            {
                objposicion = Posicion.abajo; // Cambia la dirección a abajo.
            }
        }
    }
}