export async function obtenerProductos() {
  const response = await fetch("http://localhost:5221/api/ProductoApi");
  if (!response.ok) {
    throw new Error("Error al obtener los productos");
  }
  return await response.json();
}