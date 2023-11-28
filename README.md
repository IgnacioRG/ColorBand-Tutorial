# Proyecto de Servicio Social [TUTORIAL ColorBand]

## Introducción
El juego ColorBand incorpora elementos sonoros para incrementar-fortalecer la habilidad de memoria, relacionando estímulos auditivos con elementos visuales simples (colores).

## Dinámica de Juego
ColorBand muestra en una pantalla previa al nivel un conjunto de estímulos representados como tuplas (color, sonido). El jugador debe memorizar qué color reproduce ese sonido. Mientras la dificultad aumenta, más estímulos son requeridos para memorizar. Con fines estéticos y de dinámica simple, se decidió optar por colores sólidos con una correcta asignación cromática de colores cálidos a fríos.

Durante el juego, se mostrará un escenario con elementos basados y reminiscentes a equipo electronico de música, como una barra de sonido para la cuenta regresiva, botones interactuables reminiscentes a los juegos de ritmo como Guitar Hero, PaRappa the Rapper, etc. El jugador tendrá un tiempo límite para presionar el botón correspondiente al sonido que se está reproduciendo.

## Consideraciones
El sistema dinámico de niveles permite darle un reto justo al jugador mientras va incrementando más su habilidad. Dependiendo del número de fallos en una pantalla, el nivel del jugador puede subir, bajar o permanecer en el mismo.

- El jugador cometió a lo más 1 fallo (Sube de nivel)
- El jugador cometió 2 o 3 fallos (Permanece en el mismo nivel)
- El jugador cosmetico 4 o más fallos (Baja de nivel)

## Estructura del juego
El juego se compone de un total de 10 niveles, en cada uno de ellos se modifican ciertos parámetros que aumentan la dificultad.
1. Estímulos: 2, Tiempo 10
2. Estímulos: 2, Tiempo 10
3. Estímulos: 3, Tiempo 10
4. Estímulos: 3, Tiempo 10
5. Estímulos: 4, Tiempo 8
6. Estímulos: 4, Tiempo 7
7. Estímulos: 4, Tiempo 6
8. Estímulos: 5, Tiempo 5
9. Estímulos: 5, Tiempo 5
10. Estimulos: 5, Tiempo 5

## Tutorial
El Tutorial explicacion mostrará la mecánica principal del juego y aprovechara los assets para darle una pequeña narrativa. Al ser una mecánica básica, se aprovecharán algunas animaciones para hacerla más atractiva. También se crearán assets nuevos para volver a distribuir el menú principal.

## Registro de Actividades

### 27-11-23
Guardado de la versión legado de ColorBand. Análisis de estructura, sistema de niveles y scripts principales del juego. Pruebas de jugabilidad. Se inició el diseño de la explicación tutorial.

### 28-11-23
Se creo la escena para el tutorial, ademas de la creacion de assets inspirados en Kessouku Band para Color Band. Se modifico la distribucion de elementos en la escena del menu para incorporar el boton tutorial. Se planifico el flujo tutorial con la mecanica principal en la primera y segunda fase, ademas de la explicacion del sistema dinamico.
