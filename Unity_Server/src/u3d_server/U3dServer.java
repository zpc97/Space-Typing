package u3d_server;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.BindException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;

import sql.ScoreData;

public class U3dServer implements Runnable {
	public void run() {

		ServerSocket u3dServerSocket = null;

		while (true) {
			

			try {

				u3dServerSocket = new ServerSocket(8016);

				System.out.println("u3d服务已经启动,监听8000端口");

				while (true) {
					Socket socket = u3dServerSocket.accept();
					System.out.println("客户端接入");
					System.out.println(socket.getInetAddress().getHostAddress());
					new RequestReceiver(socket).start();
				}

			} catch (BindException e1) {
				System.err.println("服务器端口被占用！");

			} catch (IOException e) {
				System.err.println("服务器接入失败");

				if (u3dServerSocket != null) {
					try {
						u3dServerSocket.close();
					} catch (IOException ioe) {
					}
					u3dServerSocket = null;
				}
			}

			// 服务延时重启
			try {
				Thread.sleep(5000);
			} catch (InterruptedException e) {

			}

		}

	}

	/**
	 * 客户端请求接收线程
	 * 
	 * @author lm
	 *
	 */
	class RequestReceiver extends Thread {

		/** 报文长度字节数 */
		private int messageLengthBytes = 1024;

		private Socket socket;

		/** socket输入处理流 */
		private BufferedInputStream bis = null;

		public RequestReceiver(Socket socket) {
			this.socket = socket;
		}

		@Override
		public void run() {
			try {
				bis = new BufferedInputStream(socket.getInputStream());
				byte[] buf = new byte[messageLengthBytes];

				/**
				 * 在Socket报文传输过程中,应该明确报文的域
				 */
				while (true) {

					/*
					 * 这种业务处理方式是根据不同的报文域,开启线程,采用不同的业务逻辑进行处理 依据业务需求而定
					 */
					// new RequestHandler(socket, buf).start();
					
					
					OutputStream os=null;
					
					os=socket.getOutputStream();
					
					String IP=socket.getInetAddress().getHostAddress();
					 
					bis.read(buf);
					String message = new String(buf, "utf-8");
					int FinalScore = 0;
					if (!message.trim().equals("") ) {
						System.out.println(message);
						if(message.substring(0, 8).equals("GameOver")) {
							String[] scoredata=message.split(";");
							for(int j=0;j<scoredata.length;j++) {
								System.out.println(scoredata[j]);
							}
							FinalScore = Integer.parseInt(scoredata[1].trim());
							
							int[] score = new int[9];
							score = ScoreData.getScore();
							int location = 0;
							for (int i = 8; i >= 0; i--) {
								if (FinalScore >= score[i]) {
									location++;
								}
							}
							if (location != 0) {
								//ScoreData.updateTable(FinalScore, 9 - location);
								ScoreData.updateScoreTable(IP, FinalScore,ScoreData.getNameByIP(IP) , 10-location);
							}
							
							
						}
						
						if(message.trim().equals("LoadScore")) {
							//int[] Intscore = new int[9];
							String[] Names=ScoreData.getRankingName();
							int[] Intscore = ScoreData.getScore();
							StringBuffer Score=new StringBuffer();
							Score.append("score is ");
							for(int i=0;i<9;i++) {
								Score.append(Names[i]+"    "+Intscore[i]+";");
							}
							os.write(Score.toString().getBytes());
							os.flush();
						}
						if(message.trim().equals("Login")) {
							//String[] IPs=ScoreData.getIP();
							ArrayList<String> IPList=ScoreData.getIP();
							int temp=0;
							for(int i=0;i<IPList.size();i++) {
								if(socket.getInetAddress().getHostAddress().equals(IPList.get(i).trim())) {
									temp++;
								}
							}
							if(temp!=0) {
								os.write("exist".getBytes());
								
							}else {
								os.write("notexist".getBytes());
							}
							os.flush();
							
						}
						if(message.substring(0,4).equals("Reg:")) {
							ArrayList<String> NameList=ScoreData.getUserName();
							boolean isExist=false;
							for(int i=0;i<NameList.size();i++) {
								if(message.trim().substring(4).equals(NameList.get(i))) {
									isExist=true;
								}
							}
							if(isExist) {
								os.write("NameRepeat".getBytes());
								os.flush();
							}else {
								ScoreData.addUser(socket.getInetAddress().getHostAddress(), message.trim().substring(4));
							}
						}
						
					}

				
					

				}

			} catch (IOException e) {
				System.err.println("读取报文出错");
			} finally {
				if (socket != null) {
					try {
						socket.close();
					} catch (IOException e) {
					}
					socket = null;
				}
			}

		}
	}
}
