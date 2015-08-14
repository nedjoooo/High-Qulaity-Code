import java.awt.Color;
import java.awt.Graphics;
import java.awt.Rectangle;


/**
 * Point holds coordinates points objects.
 * @author
 *
 */
@SuppressWarnings("unused")
public class Point {
	private int x, y;
	private final int width = 20;
	private final int height = 20;

	public Point(Point p) {
		this(p.x, p.y);
	}

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public int getPointX() {
		return x;
	}

	public void setPointX(int x) {
		this.x = x;
	}

	public int getPointY() {
		return y;
	}

	public void setPointY(int y) {
		this.y = y;
	}

	public void drawSnake(Graphics g, Color cvqt) {
		g.setColor(Color.BLACK);
		g.fillRect(x, y, width, height);
		g.setColor(cvqt);
		g.fillRect(x + 1, y + 1, width - 2, height - 2);
	}

	public String toString() {
		return "[x=" + x + ",y=" + y + "]";
	}

	public boolean equals(Object setPoint) {
		if (setPoint instanceof Point) {
			Point point = (Point) setPoint;
			return (x == point.x) && (y == point.y);
		}
		return false;
	}
}
