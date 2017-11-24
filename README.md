# Emotion Detection Using Microsoft Cognitive Service Emotion API
Emotion is detected by capturing image or by browsing image.

## Step-1
Open Visual Studio File->Project->New Project->Templates->Visual c#->Windows->Universal->Blank App. Give a name to your project then Open.

## Step-2
Right click on your solution then go to Manage NuGet Packages for solution. Then Browse and install Newtonsoft_json and Microsoft.ProjectOxford.Emotion.

## Step-3
MainPage.xaml is the layout or UI with an image and a button. Source of image is Images->emotion.jpg. Action for button is in MainPage.xaml.cs. Add a new xaml file in the project name it page1.xaml. Similar for page1.xaml as MainPage.xaml.

## Step-4
Go to [Cognitive Service](https://azure.microsoft.com/en-us/try/cognitive-services/) get Emotion API key. Then replace it with the API key in page1.xaml.cs.

## Step-5
Run the project.


![capture1](https://user-images.githubusercontent.com/22630933/33197270-9769acac-d10e-11e7-83a2-a4310a58c23f.PNG)

![capture2](https://user-images.githubusercontent.com/22630933/33197271-98b900b2-d10e-11e7-8b16-422d913d3edb.PNG)

![capture3](https://user-images.githubusercontent.com/22630933/33197275-9b03f66a-d10e-11e7-8a7f-8a3df64c2d68.PNG)
