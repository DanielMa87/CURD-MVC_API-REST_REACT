import logo from './logo.svg';
import './App.css';
import React from 'react';
import ListaProductos from './components/ListaProductos';
function App() {
  return (
    <div className="App">
      <header className="App-header">
      <div>
             <ListaProductos />
        </div>
  
      </header>
    </div>
  );
}

export default App;
