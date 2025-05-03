import React, { useEffect, useState } from 'react';
import { obtenerProductos } from '../services/ProductoService';

const ListaProductos = () => {
    const [productos, setProductos] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchProductos = async () => {
            try {
                const data = await obtenerProductos();
                setProductos(data); // Asigna los datos directamente
            } catch (error) {
                console.error('Error al cargar los productos:', error.message);
                setError('No se pudieron cargar los productos. Intenta nuevamente.');
            }
        };
        fetchProductos();
    }, []);

    return (
        <div>
            <h1>Lista de Productos</h1>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {productos.length === 0 ? (
                <p>No hay productos disponibles.</p>
            ) : (
                <ul>
                    {productos.map((producto) => (
                        <li key={producto.id}>
                            {producto.nombre} - {producto.precio} USD
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default ListaProductos;