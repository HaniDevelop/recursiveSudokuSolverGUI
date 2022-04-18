This is a WPF application that will solve any given "Super Sudoku" (16x16) board if a solution exists. The starting board can be either a default hard-coded board, or the user can click on "browse files" to navigate through their file explorer to upload a .txt file representation of a board. My program will parse through the selected file to construct a super sudoku board and display it on the left side of the WPF application. When the user clicks on "uninformed solve" the program will display the solved board on the right.

The algorithm that I use is a brute-force recursive backtracking method. It tries combinations in order, and if it reaches a dead-end (i.e. no possible valid moves for its given spot), it will backtrack and change previous values around until the entire board is solved.

Screenshots:


![Screenshot 2022-04-18 142714](https://user-images.githubusercontent.com/103938494/163881003-b01ead71-b4e0-4b3d-a4e0-42e271d3212f.png)
![Screenshot 2022-04-18 142823](https://user-images.githubusercontent.com/103938494/163881042-d90b4b0b-dcee-4b89-90a8-9878efa6a80f.png)
![Screenshot 2022-04-18 142908](https://user-images.githubusercontent.com/103938494/163881079-258c4837-4be9-4ec5-8d1a-c99b303b61de.png)
