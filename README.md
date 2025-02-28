Este archivo implementa un juego de gusano (Snake) utilizando Windows Forms en C# y .NET 8.0. A continuación se detallan las características técnicas del programa:

•	Clase Form1: Hereda de Form y contiene la lógica principal del juego.
•	Enumeración Posicion: Define las direcciones posibles del gusano (izquierda, derecha, arriba, abajo).
Atributos:
•	List<Point> snake: Representa el cuerpo del gusano.
•	Point food: Representa la posición de la comida.
•	Posicion objposicion: Dirección actual del gusano.
•	int gridSize: Tamaño de la cuadrícula (10 píxeles).
•	List<Point> particles: Lista de partículas generadas al comer la comida.
•	System.Windows.Forms.Timer particleTimer: Temporizador para controlar la duración de las partículas.
•	Constructor Form1(): Inicializa los componentes del formulario, configura la ventana, inicializa el gusano, la comida y las partículas, y configura el temporizador de partículas.
Métodos:
•	timer1_Tick(object sender, EventArgs e): Maneja el evento Tick del temporizador principal, mueve el gusano, verifica colisiones y redibuja el formulario.
•	MoveSnake(): Mueve el gusano en la dirección actual y maneja la lógica de crecimiento al comer la comida.
•	CheckCollision(): Verifica colisiones con los bordes del formulario y con el propio cuerpo del gusano.
•	GenerateFood(): Genera la comida en una posición aleatoria dentro de la cuadrícula.
•	GenerateParticles(Point position): Genera partículas en la posición de la comida.
•	ParticleTimer_Tick(object sender, EventArgs e): Maneja el evento Tick del temporizador de partículas, limpiando la lista de partículas y deteniendo el temporizador.
•	GameOver(): Detiene el juego y muestra un mensaje de fin de juego, permitiendo al usuario reiniciar o salir.
•	RestartGame(): Reinicia el estado del juego, incluyendo la posición del gusano y la comida.
•	Form1_Paint(object sender, PaintEventArgs e): Dibuja el gusano, la comida y las partículas en el formulario.
•	Form1_KeyDown(object sender, KeyEventArgs e): Maneja los eventos de teclado para cambiar la dirección del gusano.


El archivo está diseñado para ser ejecutado en un entorno .NET 8.0 y utiliza características de C# 12.0.
