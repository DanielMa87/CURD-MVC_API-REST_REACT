import axios from 'axios';

const API_URL = 'http://localhost:5221/api/ProductoApi'; // Cambia el puerto si es necesario

export const obtenerProductos = async () => {
    try {
        const response = await axios.get('http://localhost:5221/api/ProductoApi'); // Cambia la URL si es necesario
        return response.data;
    } catch (error) {
        console.error('Error al obtener productos:', error.message);
        throw error; // Asegúrate de que el error sea manejado en el componente
    }
};

export const obtenerProductoPorId = async (id) => {
    try {
        const response = await axios.get(`${API_URL}/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error al obtener el producto:', error);
        throw error;
    }
};

export const crearProducto = async (producto) => {
    try {
        const response = await axios.post(API_URL, producto);
        return response.data;
    } catch (error) {
        console.error('Error al crear el producto:', error);
        throw error;
    }
};

export const actualizarProducto = async (id, producto) => {
    try {
        const response = await axios.put(`${API_URL}/${id}`, producto);
        return response.data;
    } catch (error) {
        console.error('Error al actualizar el producto:', error);
        throw error;
    }
};

export const eliminarProducto = async (id) => {
    try {
        const response = await axios.delete(`${API_URL}/${id}`);
        return response.status === 204;
    } catch (error) {
        console.error('Error al eliminar el producto:', error);
        throw error;
    }
};