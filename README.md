# MiniProject_DeskTop


## Wpf MovieFinderApp
그동안 배웠던 WPF를 활용해 MovieFinderApp을 만들어 보았습니다. MetroMahApp과 IconPack을 이용해 디자인적 요소를 넣고 XML의 형식으로 데이터에 요소를 바인딩하여 다양한 데이터 소스들을 활용해 보았습니다. 또한 네이버영화의 OpenApi, 유튜브의 OpenApi를 통해 데이터를 받고 그 데이터를 가공하여 영화의 정보를 DataGrid영역과 ListView영역에 배치해 보았습니다. </br> 하단의 버튼들을 통해 즐겨찾기 추가, 즐겨찾기 보기, 즐겨찾기 삭제가 가능하게 만들었고 유튜브보기 버튼은 유튜브의 OpenApi로 영화의 티져를 가져와 자동재생을 시켜주는 기능을 넣었습니다. 마지막에 있는 네이버 영화버튼은 영화를 선택하고 버튼을 누르면 해당영화의 네이버 영화 URL로 바로가는 기능을 넣었습니다. 

------------
### 1. 초기화면

![Start_Image](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160424_001.png)

초기의 화면에는 NO_Pictuer이미지를 삽입했습니다.

<br/>
<br/>

### 2. 영화검색

![Search_Image](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160456_001.png)

TextBox에 '마션'이라는 영화를 검색했습니다. 검색할 때 문자를 한글부터 나오게 설정했으며 아래 StatusBar를 통해 검색호출 상태를 보이게 만들었습니다.

<br/>
<br/>

### 3. 즐겨찾기추가

![Add_Favorit_Image1](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160409_001.png)

![Add_Favorit_Image2](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160435_001.png)

앞에 검색했던 '마션'을 즐겨찾기에 추가한 모습입니다.(위) "즐겨찾기를 성공했습니다" 라는 메세지와 함께 즐겨찾기가 추가 된 모습을 볼 수 있습니다.(아래)

<br/>

![Add_Empty](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160450_001.png)

만약 영화를 선택하지 않고 즐겨찾기추가 버튼을 누르면 오류 메세지가 뜨고 "즐겨찾기에 추가할 영화를 선택하세요 (중복선택가능)" 이라는 메세지가 뜨도록 설정했습니다.

<br/>
<br/>

### 4. 즐겨찾기 삭제

![Delete_FavorList1](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160404_001.png)

영화를 선택하지않고 즐겨찾기 삭제를 누르면 나오는 메세지입니다.

<br/>

![Delete_FavorList2](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160423_001.png)

영화 '마션'이 즐겨찾기에 삭제되었습니다.

<br/>

![Delete_FavorList3](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160410_001.png)

삭제가 되면 삭제가 되었다는 확인 메세지가 나옵니다. 

<br/>

![Delete_FavorList4](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_180431_001.png)

MovieFinderApp과 DB와 연동된 모습입니다. DB에는 RegDate를 넣어 즐겨찾기에 추가한 날짜를 알 수 있게 만들었습니다.

<br/>
<br/>


### 5. 유튜브 영화보기


![[Youtube_Watch_Image1](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160453_001.png)

즐겨찾기를 조회한 부분에서 영화 '미나리' 선택해 유튜브 영화보기 버튼을 눌러보겠습니다.

<br/>

![Youtube_Watch_Image2](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160426_001.png)

새로운 페이지를 생성하고 그 안에 ListView와 WebBrowser를 연동했습니다. ListView의 앞쪽에는 유튜브 섬네일이미지를 배치하였고 타이틀, 제작자, 링크 순으로 정보가 나오게 만들었습니다. 또한, 오른쪽 WebBrower부분에는 해당 영화의 티져를 불러들여 자동재생이 될 수 있도록 만들었습니다.

<br/>
<br/>

### 6. 네이버 영화보기

![Naver_Watch_Image](https://github.com/zizi0308/MiniProject_DeskTop/blob/main/WpfMiniProject/Images/img_20210405_160443_001.png)

영화 '미나리'의 네이버 영화보기 버튼을 클릭한 모습입니다. 클릭하는 즉시 바로 해당 네이버 영화의 URL로 이동하는 것을 볼 수 있습니다.





