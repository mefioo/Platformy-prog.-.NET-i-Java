package Action;
import java.awt.FlowLayout;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import javax.swing.*;
import java.util.Random;

import javax.swing.JFrame;

public class Main {

	public static void main(String[] args)
	{
		Window go = new Window();
		go.setExtendedState(JFrame.MAXIMIZED_BOTH); 
		go.setUndecorated(true);
		go.setVisible(true);
		
	}
}
