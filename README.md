# FunctionNN
Function Neural Network is a simple demo of a neural network that is able to learn to 'imitate' any two variable contiunous function in a defined domain.
The demo includes manual weight controls, learning options, a visualization of the neural network as well as a comparative visualization of the function to imitate and the neural network calculations.
See [NN Design.pdf](https://github.com/eberjair/FunctionNN/blob/master/NN%20Design.pdf) for the details of the neural network design and its learning process.
See [App Manual.pdf](https://github.com/eberjair/FunctionNN/blob/master/App%20Manual.pdf) for a brief manual of the application.

## Adding functions
To add more possible functions for the neural network to imitate you can do it in the method `void AddFunctions()` in [MainWindow.xaml.cs](https://github.com/eberjair/FunctionNN/blob/master/FunctionNeuralNetwork/MainWindow.xaml.cs) along with its implementation in [/Functions/FunctionsImplementations.cs](https://github.com/eberjair/FunctionNN/blob/master/FunctionNeuralNetwork/Functions/FunctionsImplementations.cs).

## Contribuiting
Pull request are welcome. For major changes, please open an issue to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
