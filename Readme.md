# 中華一翻系統建置
1. 安裝xampp: https://www.apachefriends.org/zh_tw/index.html
完成後開啟xampp control panel 將apache與MySql都開啟
![](https://i.imgur.com/8NNKm93.png)

2. 設置為外部ip連線
將xampp Apache的config點開，選擇Apache(http.xampp.conf)之後會出現一個文檔，到最下面尋找Require local字串，並加它改成Require all granted，儲存 重開apache即生效，
可嘗試在本機瀏覽器輸入http://本機電腦ip/phpmyadmin/ 有出現phpmyadmin網頁則代表成功

![](https://i.imgur.com/o4GWsfT.png)

3. 創建資料庫帳戶
點選MySql的Admin確認可以連線本機資料庫
http://localhost/phpmyadmin/
在使用者帳號內幫自己創一個擁有全域權限的帳號
![](https://i.imgur.com/lKbKG1L.png)

4. 執行撈資料的php

   4.1. 進到安裝xampp的資料夾，尋找名為htdocs的資料夾並進入
  ![](https://i.imgur.com/rQU34Xe.png)

   4.2. 將要執行的php檔案夾丟進去(如圖中的quik)
  開啟quik資料夾中connect.php檔案，並更改host為你要連的ip(本機為127.0.0.1)，將id與pwd都更改為你剛創的使用者名稱與密碼，dbName也改為你想要連的資料庫名稱
  <!-- ![](https://i.imgur.com/0uE65MM.png) -->
   
   4.3. 載瀏覽器中輸入 http://127.0.0.1/quik/connect.php
  若為空白則表示連接成功

5. 多人連線系統建置
可先參考這篇如何安裝與啟動
https://3dactionrpg.blogspot.com/2018/05/6-1-photon-server-by-self-hosted.html?m=1&fbclid=IwAR3gGA9Pt53CCx_Tc24dZGnGIMdk63co375i4XAt0-8ZN1vHU0OvmFuqFoc
當執行unity後發現仍然無法實現多人連線 出現以下錯誤訊息
![](https://i.imgur.com/kdk3nkr.jpg)
到PhotonControl menu的game server ip config設定公開ip
![](https://i.imgur.com/bIjRogi.png)
