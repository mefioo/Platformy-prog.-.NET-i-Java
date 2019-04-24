package Action;

import java.awt.FlowLayout;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import javax.swing.*;
import java.util.Random;


public class Window extends JFrame
{
	

	JButton reg;
	Random rand = new Random();
	public Window()
	{
		super("Przycisk");
		this.setLayout(null);
		int rand1 = rand.nextInt(500)+50;
		int rand2 = rand.nextInt(500)+50;
		
		reg = new JButton("xD");
		reg.setSize(50,50);
		reg.setLocation(rand1,rand2);
		add(reg);
		
		Handler handler = new Handler(this,reg);
		reg.addActionListener(handler);

		
	}
	


}