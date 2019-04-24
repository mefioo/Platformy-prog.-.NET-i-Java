package Action;

import java.awt.FlowLayout;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import javax.swing.*;
import java.util.Random;

public class Handler implements ActionListener
{
	
	public Window oknoZycia;
	private JButton r;
	private Random rand = new Random();
	
	public Handler(Window window,JButton r) {
		
		this.oknoZycia=window;
		this.r=r;
	} 
	
	public void actionPerformed(ActionEvent event)
	{
		int rand1 = rand.nextInt(oknoZycia.getBounds().height-25);
		int rand2 = rand.nextInt(oknoZycia.getBounds().width-25);
		r.setLocation(rand2,rand1);
	}
}