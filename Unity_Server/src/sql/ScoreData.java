package sql;

import sql.Database;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

public class ScoreData {
	private static Connection connection = Database.getConnection();
	private static Statement stmt;
	private static ResultSet rs;

	public static void init() {
		try {
			stmt = connection.createStatement();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public static int[] getScore() {
		init();
		int[] score = new int[9];
		try {
			rs = stmt.executeQuery("use zpc;select Score from score_Table;");
			int temp = 0;
			while (rs.next()) {
				score[temp] = rs.getInt("score");
				temp++;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return score;
	}
	public static String[] getRankingName() {
		init();
		String[] names=new String[9];
		try {
			rs = stmt.executeQuery("use zpc;select Name from score_Table;");
			int temp = 0;
			while (rs.next()) {
				names[temp] = rs.getString("Name");
				temp++;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return names;
	}
	public static ArrayList<String> getIP() {
		init();
		ArrayList<String> IPList=new ArrayList<String>();
		try {
			rs = stmt.executeQuery("use zpc;select IP from User_Table;");
			int temp = 0;
			while (rs.next()) {
				IPList.add(rs.getString("IP"));
				temp++;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return IPList;
	}
	public static ArrayList<String> getUserName() {
		init();
		ArrayList<String> NameList=new ArrayList<String>();
		try {
			rs = stmt.executeQuery("use zpc;select Name from User_Table;");
			int temp = 0;
			while (rs.next()) {
				NameList.add(rs.getString("Name"));
				temp++;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return NameList;
	}
	
	public static int TableLength() {
		init();
		int temp = 0;
		try {
			// stmt=connection.createStatement();
			rs = stmt.executeQuery("use zpc;select Score from score_Table;");
			while (rs.next()) {
				temp++;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
//		System.out.println(temp);
		return temp;
	}
	public static void updateScoreTable(String IP,int score,String name,int location) {
		init();
		try {
			stmt.executeUpdate("use zpc;update score_Table set IP='"+IP+"',Name='"+name+"',Score="+score+" where ID="+location+";");
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public static void addUser(String IP,String Name) {
		init();
		try {
			stmt.executeUpdate("use zpc;insert into User_Table Values ('"+IP+"','"+Name+"');");
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	public static String getNameByIP(String IP) {
		init();
		String Name="";
		try {
			rs = stmt.executeQuery("use zpc;select * from User_Table;");
			int temp = 0;
			System.out.println("in:"+IP);
			while (rs.next()) {
				System.out.println(rs.getString("IP").trim()+"out");
				if(IP.equals(rs.getString("IP").trim())) {
					Name=rs.getString("Name");
					System.out.println("13");
				}
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return Name;
	}
	
	
	public boolean isExist() {
		return false;
	}
}
