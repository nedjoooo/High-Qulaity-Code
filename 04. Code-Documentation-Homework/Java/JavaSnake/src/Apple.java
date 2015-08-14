import java.awt.Color;
import java.awt.Graphics;
import java.util.Random;


/**
 * Class Apple generate apple and paint in to board with given points.
 * @author
 *
 */
public class Apple {
	public static Random aplleGenerator;
	private Point apple;
	private Color snakeColor;

	public Apple(Snake s) {
		apple = createApple(s);
		snakeColor = Color.RED;
	}

	private Point createApple(Snake snake) {
		aplleGenerator = new Random();
		int x = aplleGenerator.nextInt(30) * 20;
		int y = aplleGenerator.nextInt(30) * 20;
		for (Point snakePoint : snake.snakeBody) {
			if (x == snakePoint.getPointX() || y == snakePoint.getPointY()) {
				return createApple(snake);
			}
		}
		return new Point(x, y);
	}

	public void drawApple(Graphics g) {
		apple.drawSnake(g, snakeColor);
	}

	public Point getPoint() {
		return apple;
	}
}
