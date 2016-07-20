package main;

import java.sql.*;
import java.io.*;
import java.util.*;

//import com.braju.format.*;

public class FindSeisakuAsKounyuYosanBy {

	// DBの接続設定
	private static Connection con = null;
	private PreparedStatement[] pstmt = new PreparedStatement[1];

	protected static String enCode; // ファイルエンコード．
	private String sql_date = "";
	private String sql_time = "";

	public static void main(String[] args) {
		try {
			if (args.length < 3) {
				System.out.println(
						"Parameter error!! args.length : " + args.length +"\n" +
								"	Param1 : 検索方法 	\n" +
								"	Param2 : 入力ファイル名	\n" +
								"	Param3 : 出力ファイル名	\n" +
								"	Param4 : 設定ファイル名	\n" +
						"を指定して下さい。終了します。");
				System.exit(1);
			}
			String findMode = args[0];
			String inputFileName  = args[1];
			String outputFileName = args[2];
			String setupFileName  = args[3];

			new FindSeisakuAsKounyuYosanBy().start(findMode, inputFileName, outputFileName, setupFileName);

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private void start(String findMode, String inputFileName, String outputFileName,String setupFileName)
			throws Exception {
		try {
			// 日時を取得
			sql_date = get_sqldate();
			sql_time = get_sqltime();
			System.out.println("= リスト作成開始 = [" + sql_date + " " + sql_time + "]");

			// 出力ファイル削除
			delOutputFile(outputFileName);

			// DB接続
			connect(setupFileName);

			// プリペア文の登録
			setPrepare(findMode);

			// リスト作成
			selectSettsuNoList(findMode, inputFileName, outputFileName);

			// DB切断
			close();

			// 日時を取得
			sql_date = get_sqldate();
			sql_time = get_sqltime();
			System.out.println("= リスト作成終了 = [" + sql_date + " " + sql_time + "]");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// ファイル削除
	private void delOutputFile(String delete_file) throws Exception {
		try {
			boolean exst = false;
			boolean delfile = false;

			File file = new File(delete_file);
			exst = file.exists();

			if (exst == true) {
				delfile = file.delete();
			}
			System.out.println("\nファイル削除[" + delete_file + "] ⇒ [" + delfile + "]\n");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// プリペア文の登録
	private void setPrepare(String findMode) throws SQLException {
		StringBuffer bufSql = new StringBuffer(1028);
		try {

			bufSql.delete(0, bufSql.length());
			if( findMode.equals("1") ) {
				bufSql.append("SELECT");
				bufSql.append(" CASE WHEN EQANS1 > 0 THEN EQANS1 ELSE SDYOYM END AS KOUNYU_YOSAN_YYYY_MM");
				bufSql.append(" ,SUM(MIMXBH) AS BUHIN_HIREI_HI_COUNT");
				bufSql.append(" ,SUM(MIMXKH) AS BUHIN_KOTEI_HI_COUNT");
				bufSql.append(" FROM CYNLIBF.CYNAA010");
				bufSql.append(" WHERE");
				bufSql.append(" SUBSTR(SGISBA,1,3) = ? ");
				bufSql.append(" AND HCJYOK <> '取消' ");
				bufSql.append(" AND HCJYOK <> '見積取消' ");
				bufSql.append(" GROUP BY CASE WHEN EQANS1 > 0 THEN EQANS1 ELSE SDYOYM END");
			} else {
				bufSql.append("SELECT");
				bufSql.append(" MAX(CASE WHEN EQANS1 > 0 THEN EQANS1 ELSE SDYOYM END) AS KOUNYU_YOSAN_YYYY_MM1");
				bufSql.append(" ,MIN(CASE WHEN EQANS1 > 0 THEN EQANS1 ELSE SDYOYM END) AS KOUNYU_YOSAN_YYYY_MM2");
				bufSql.append(" FROM CYNLIBF.CYNAA010");
				bufSql.append(" WHERE");
				bufSql.append(" SUBSTR(SGISBA,1,3) = ? ");
				bufSql.append(" AND HCJYOK <> '取消' ");
				bufSql.append(" AND HCJYOK <> '見積取消' ");
				bufSql.append(" AND SDYOYM > 0");
			}
			pstmt[0] = con.prepareStatement(bufSql.toString());

			
		} catch (SQLException e) {
			while (e != null) {
				System.err.println(e.getMessage());
				System.err.println(e.getSQLState());
				System.err.println(e.getErrorCode());
				System.out.println("");
				e = e.getNextException();
			}
		}
	}

	private void selectSettsuNoList(String findMode, String inputFileName, String outputFileName)
			throws SQLException {
		String s;
		String buff = "";
		ResultSet rs = null;
		int i = 0;

		try {
			// 入力ファイルの定義
			FileInputStream fis = new FileInputStream(inputFileName);
			InputStreamReader isr = new InputStreamReader(fis, "MS932");
			BufferedReader br = new BufferedReader(isr);

			// 出力ファイルの定義
			FileOutputStream fos = new FileOutputStream(outputFileName);
			OutputStreamWriter osw = new OutputStreamWriter(fos, "MS932");
			BufferedWriter bw = new BufferedWriter(osw);

			// 入力ファイル読込み
			while ((buff = br.readLine()) != null) {
				System.out.println("\nbuff = [" + buff + "]\n");
				pstmt[0].setString(1, buff.trim());

				rs = pstmt[0].executeQuery();
				if ( findMode.equals("1") ) {
					while (rs.next()) {
						s = String.format("%d\t%f\t%f\n",
								rs.getInt("KOUNYU_YOSAN_YYYY_MM"),
								rs.getBigDecimal("BUHIN_HIREI_HI_COUNT"),
								rs.getBigDecimal("BUHIN_KOTEI_HI_COUNT"));
						bw.write(convertSJISto932(s));
					}
				} else {
					while (rs.next()) {
						s = String.format("%d\t%d\n",
								rs.getInt("KOUNYU_YOSAN_YYYY_MM1"),
								rs.getInt("KOUNYU_YOSAN_YYYY_MM2"));
						bw.write(convertSJISto932(s));
					}
				}
					 
				rs.close();
				

			}
			br.close();
			bw.close();

		} catch (SQLException e) {
			while (e != null) {
				System.err.println(e.getMessage());
				System.err.println(e.getSQLState());
				System.err.println(e.getErrorCode());
				System.out.println("");
				e = e.getNextException();
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				for (i = 0; i < pstmt.length; i++) {
					if (pstmt[i] != null) {
						pstmt[i].close();
					}
				}
			} catch (SQLException e) {
				System.err.println(e.getMessage());
				System.err.println(e.getSQLState());
				System.err.println(e.getErrorCode());
				System.out.println("");
				e = e.getNextException();
			}
		}
	}

	// DB接続
	private void connect(String setupFileName) throws Exception {
		String driver;
		String url;
		String user;
		String pass;
		Properties prop = new Properties(); // �v���p�e�B�̃C���X�^���X�쐬�D
		prop.load(new FileInputStream(setupFileName));
		
		driver		= prop.getProperty("partsDriver");
		url			= prop.getProperty("partsDb");
		user		= prop.getProperty("partsUid");
		pass		= prop.getProperty("partsPw");
		
		Driver drv = (Driver) (Class.forName(driver).newInstance());
		DriverManager.registerDriver(drv);
		con = DriverManager.getConnection(url, user, pass);
	}

	// DB切断
	private void close() throws SQLException {
		if (con != null)
			con.close();
	}

	// 年月日取得(SQL型)
	private String get_sqldate() {
		String buff = "";
		Calendar cal1 = Calendar.getInstance();
		int year = cal1.get(Calendar.YEAR);
		int month = cal1.get(Calendar.MONTH) + 1;
		int day = cal1.get(Calendar.DATE);
		buff = String.format("%04d-%02d-%02d", year, month, day);
		return (buff);
	}

	// 時分秒取得(SQL型)
	private String get_sqltime() {
		String buff = "";
		Calendar cal1 = Calendar.getInstance();
		int hour = cal1.get(Calendar.HOUR_OF_DAY);
		int minute = cal1.get(Calendar.MINUTE);
		int second = cal1.get(Calendar.SECOND);
		buff = String.format("%02d:%02d:%02d", hour, minute, second);
		return (buff);
	}

	private static String convertSJISto932(String str) {
		String out = null;

		int len = str.length();
		StringBuffer buf = new StringBuffer(len);
		/*
			文字 SJISからUnicodeへのマッピング CP932からUnicodeへのマッピング 
		― 0x2015 0x2014 
		～ 0x301c 0xff5e 
		∥ 0x2016 0x2225 
		－ 0x2212 0xff0d 
		￠ 0x00a2 0xffe0 
		￡ 0x00a3 0xffe1 
		￢ 0x00ac 0xffe2 
	    */

		for (int i = 0; i < len; i++) {
			char c = str.charAt(i);
			switch (c) {
			case '\u2015':
				c = '\u2014';
				break;
			case '\u301C':
				c = '\uFF5E';
				break;
			case '\u2016':
				c = '\u2225';
				break;
			case '\u2212':
				c = '\uFF0D';
				break;
			case '\u00A2':
				c = '\uFFE0';
				break;
			case '\u00A3':
				c = '\uFFE1';
				break;
			case '\u00AC':
				c = '\uFFE2';
				break;
			}
			buf.append(c);
		}
		out = buf.toString();
		return out;
	}
}
