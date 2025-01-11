# PvsZ
## 基礎介紹
這是一個第一人稱的 3D 植物大戰殭屍遊戲，玩家會操控一顆鳳梨，手拿著豌豆槍，射擊5條道路上出生的殭屍，從 level 1 打至 level 5 就算通關，若是讓殭屍進到家門便會扣除血量，血量歸零便輸了。
遊戲過程中擊殺殭屍可以獲得能量，可以利用這些能量放置不同的道具來協助通關。

## 關卡機制
一關有 3 分鐘以上，每 10 秒會生成對應 level 的殭屍數量 (level 1 : 1隻 / 10s, level 2 : 2隻 / 10s, ... , level 5 : 5隻 / 10s)  

每過 1 分鐘會迎來一波屍潮，持續 10 秒，屍潮之下每 1 秒會生成對應 level 的殭屍數量  

第 3 波屍潮過後就不會再生出殭屍，直到場中沒有殭屍之後才會進入下一關 (level 5 打完就 win ~~~)

## 遊戲操作
* 使用 WASD 來控制角色移動
* 滑鼠左鍵射擊
* 滑鼠右鍵放置道具
* 滑鼠滾輪或數字鍵切換道具欄
* ESC 暫停遊戲

### 子彈
> ATK : 20
> SPE : 20

## 殭屍種類
* 普通殭屍 - 非常普通的殭屍
  > HP : 100  
  > ATK : 20  
  > SPE : 0.5  
  > Energy Give : 1  
* 奔跑殭屍 - 移動速度較快，半血以下會爆衝
  > HP : 80  
  > ATK : 15  
  > SPE : 0.75 (半血下 2.75)  
  > Energy Give : 3  
* 巨人殭屍 - 行走緩慢，攻擊力較高
  > HP : 400  
  > ATK : 25  
  > SPE : 0.35  
  > Energy Give : 10  
* 礦工殭屍 - 從地下潛行，在此期間子彈攻擊不到礦工殭屍，到家門口之後會爬起來並往遠離家門口的方向前進
  > HP : 120  
  > ATK : 20  
  > SPE : 0.5  
  > Energy Give : 1  

## 道具種類
* 向日葵 - 生產能量
  > HP : 100  
  > Energy Cost : 10  
  > Energy Give : 5 / 20s
* 豌豆射手 - 固定間隔時間下朝前方射擊豌豆
  > HP : 100  
  > Energy Cost : 20  
  > Attack Interval : 3s  
* 食人花 - 固定間隔時間下吃下範圍內最接近的殭屍
  > HP : 100  
  > Energy Cost : 30  
  > Attack Interval : 5s  
* 堅果牆 - 有較高的血量可以抵擋殭屍
  > HP : 400  
  > Energy Cost : 20  
* 辣椒 - 放下後可以清空整條道路的殭屍跟植物，但不會返還殭屍死亡的能量，請謹慎使用
  > Energy Cost : 50  
* WannaPlayApex - 原測試用道具，修改後可以充當低配版堅果牆
  > HP : 100  
  > Energy Cost : 0

## 使用版本
* Unity 6000.0.30f1
* 3D (Built-In Render Pipeline)
* Cinemachine 3.1.2

## Assets 列表
* Zombie (https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232)
* Lowpoly House Plants - Free (https://assetstore.unity.com/packages/3d/props/interior/lowpoly-house-plants-free-191367)
* Stone UI (https://assetstore.unity.com/packages/2d/gui/icons/stone-ui-182526)
* Low Poly 3D Animated Nut Brothers - Curling Package (https://assetstore.unity.com/packages/3d/characters/humanoids/low-poly-3d-animated-nut-brothers-curling-package-218199#content)
* Match 3d Object Pack: Fruits & Vegetables (https://assetstore.unity.com/packages/3d/props/food/match-3d-object-pack-fruits-vegetables-284706)
* Free Low Poly Nature Forest (https://assetstore.unity.com/packages/3d/environments/landscapes/free-low-poly-nature-forest-205742)
