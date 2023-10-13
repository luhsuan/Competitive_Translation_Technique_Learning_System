# Digital Game-based Translation Techniques Learning Platform Setup 

The multiplayer online competitive game learning system was developed using Unity and C# to connect to a MySQL database. Its purpose is to help students learn multiple translation techniques, regardless of their performance goals.

1. Install XAMPP: https://www.apachefriends.org/index.html 
After installation, open the XAMPP control panel and start both Apache and MySQL.
![](https://i.imgur.com/8NNKm93.png)

2. Configure for External IP Access
Open the XAMPP Apache configuration by selecting Apache (http.xampp.conf). Inside the document, find the Require local string and change it to Require all granted. Save the file, and restart Apache to apply the changes. You can test if it worked by opening a web browser on your local machine and entering http://yourcomputerip/phpmyadmin/. If you see the phpMyAdmin webpage, it means the configuration was successful.

![](https://i.imgur.com/o4GWsfT.png)

3. Create a Database Account
Click on MySQL's admin page to confirm you can connect to the local database: http://localhost/phpmyadmin/. Within the User Accounts section, create an account with global privileges for yourself.
![](https://i.imgur.com/lKbKG1L.png)

4. Execute PHP Scripts to Fetch Data

   4.1. Navigate to the XAMPP installation directory and find the htdocs folder. Enter this folder.
  ![](https://i.imgur.com/rQU34Xe.png)

   4.2. Place the PHP script folder you want to run (e.g., 'quik') inside 'htdocs'. Open the connect.php file inside the 'quik' folder, and change the host to the IP you want to connect to (use '127.0.0.1' for local). Update the 'id' and 'pwd' to your newly created username and password, and set 'dbName' to the database name you want to connect to.
  <!-- ![](https://i.imgur.com/0uE65MM.png) -->
   
   4.3.  In your web browser, enter http://127.0.0.1/quik/connect.php. If it's blank, the connection is successful.

5. Multiplayer System Setup
Refer to this guide on how to install and start a self-hosted Photon server: 
https://3dactionrpg.blogspot.com/2018/05/6-1-photon-server-by-self-hosted.html?m=1&fbclid=IwAR3gGA9Pt53CCx_Tc24dZGnGIMdk63co375i4XAt0-8ZN1vHU0OvmFuqFoc
If, after running Unity, you encounter issues with multiplayer connections and receive the following error messages.
![](https://i.imgur.com/kdk3nkr.jpg)
Go to PhotonControl menu and configure the public IP for the game server.
![](https://i.imgur.com/bIjRogi.png)
