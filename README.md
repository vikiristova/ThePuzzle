## Сложувалка
Едноставна имплементација на играта сложувалка која нуди избор на категорија.

 Windows Forms project by: 
- **Nikola Nikolovski** https://github.com/Amigo96
- **Viktorija Ristova** https://github.com/vikiristova

Сложувалката е игра или проблем што ја тестира способноста на една личност за знаење, препознавање шифри (patterns), и инвентивност. 
Од корисникот се бара да ги подреди делчињата на логичен начин со цел да се совпаднат со оригиналот. 
Имплементацијата се заснова на т.н. комбинациски сложувалки односно сет од парчиња со кои по пат на манипулација се доаѓа до различни комбинации. 
Операцијата искористена за донесување на некаква комбинација како состојба на сложувалката е поместување на координатите на копчето кое е кликнато и кое се наоѓа до празното поле. 
Корисничкото сценарио во нашата имплементација започнува со избор на категорија:
1. Уметнички дела од познати автори
2. Познати места во светот
![1](https://cloud.githubusercontent.com/assets/18376601/26527537/51ed0df0-4396-11e7-8924-3361094fe7eb.png)
Откако корисникот ќе се одлучи за категорија од која ќе сака да составува слика со помош на кодот енкапсулиран
во функцијата Image imageChosen(string chosen) од податочните структури **(во случајов ArrayLists од string-ови)** 
во кои го чуваме source-от на сликите околу кои се врти главната приказна на проектот, **рандом се генерира _индекс_ во низата со патеки 
до сликите**. На клик на копчето _Започни_ целната слика се сецка на 8 парчиња ( и едно празно поле), со димензии на копчињата 90x90.

```c#
       private void cropGoalImage(Image goalImage, int width, int height)
        {
            Bitmap bitmapa = new Bitmap(width,height);
            Graphics g = Graphics.FromImage(bitmapa);
            g.DrawImage(goalImage, 0, 0, width, height);
            g.Dispose();
            int moveRight = 0;
            int moveDirection = 0;
            for (int x = 0; x < 8;x++)
            {
                Bitmap partialImage = new Bitmap(90,90);
                for (int i = 0; i < 90;i++ )
                {
                    for (int j = 0; j < 90; j++) { 
                    partialImage.SetPixel(i,j,bitmapa.GetPixel(i+moveRight,j+moveDirection));
                    }
                }
                images.Add(partialImage);
                moveRight += 90;
                if (moveRight == 270) {
                    moveRight = 0;
                    moveDirection += 90;
                    
                }
            }
        }
```

**Рандом разместување на поделените парчиња од оригиналната слика** се прави во функцијата partialImagesAsButtons(ArrayList images)

```c#
 private void partialImagesAsButtons(ArrayList images)
        {
           int i=0;
           int[] array = { 0, 1, 2, 3, 4, 5, 6, 7 };
           array = shuffle(array);
           foreach (Button b in pnlSlozuvalka.Controls)
           {
               if (i < array.Length)
               {
                   b.Image = (Image)images[array[i]];
                   i++;
               }
           }
        }
```
Пример почетна состојба на сложувалката

![2](https://cloud.githubusercontent.com/assets/18376601/26527703/e849125a-4399-11e7-9a1a-a52df0872c0c.png)

Со модулот moveButton(Button button) се овозможува **поместување на копчињата _соседни_ на празното поле на клик настан**.

```c#
  private void moveButton(Button button)
        {
            if (((button.Location.X == point.X - 90 || button.Location.X == point.X + 90)&&button.Location.Y==point.Y)||(button.Location.Y == point.Y - 90 || button.Location.Y == point.Y + 90)&&button.Location.X==point.X)
            {
                Point swap = button.Location;
                button.Location = point;

                point = swap;

                updateCounter();
            }
            if(point.X==180&&point.Y==180)
            {
                validationCheck();
            }
        }
```
Додека корисникот се обидува да ја реши загатката во форма на сложувалка за да добие адреналинско чувство и да се инкрементира
факторот на гејмификација, паралелно *се декрементира бројот на преостанати чекори* кои може да ги направи за да сложувалката да 
премине во _целната состојба_. Модулот validationCheck() утврдува **дали копчињата се наоѓаат во посакуваната состојба** и заедно со 
бројачот може да резултира со:
![3](https://cloud.githubusercontent.com/assets/18376601/26527731/74c325f4-439a-11e7-9a00-029b4765295b.jpg)

или

![4](https://cloud.githubusercontent.com/assets/18376601/26527744/9a86b346-439a-11e7-872b-d456fd761bde.png)
Дополнителна функционалност за започнување од почеток се овозможува со кликање на копчето Од почеток на кое се рестартира апликацијата.
На корисникот му се дава **помош** во форма на слика со мали димензии (thumbnail) во левиот дел од формата.

