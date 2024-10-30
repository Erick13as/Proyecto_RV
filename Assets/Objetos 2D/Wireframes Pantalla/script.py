import os
from PIL import Image, ImageDraw

# Directorio base donde están las carpetas con las imágenes
base_dir = r"\VR\Proyecto_RV\Assets\Objetos 2D\Wireframes Pantalla"  # Usa 'r' para evitar problemas con las barras invertidas

# Lista de carpetas con las imágenes
folders = [
    "0.0 Portada", "0.1 Aprender a usar Botones", "0.2 Aprender a maniobrar objetos", 
    "0.3 Apreder a maniobrar objetos", "0.4 Sendoff", "1.1 Introducción a la actividad", 
    "1.2 Equipo de cocina", "1.2 Equipo de cocina - cambiar", "2.1 Encender el sartén", 
    "2.2 Importancia de precalentar el sartén", "2.3 Importancia de intensidad del fuego", 
    "3.1 Conoce la receta", "3.2 Poner Tortilla", "3.3 Poner frijoles", "3.4 Poner queso", 
    "3.5 Poner carne", "3.5 Poner carne - cambiar", "3.6 Poner más queso", "3.7 Cerrar quesadilla", 
    "4.1 Quesadilla al sartén", "4.2 Cocinar quesadilla", "4.2 Cocinar quesadilla - cambiar", 
    "4.3 Voltear quesadilla", "5.1 Servir quesadilla", "5.1 Servir quesadilla - se puede cambiar", 
    "5.2 Finalizar clase"
]

# Tamaños y posiciones para los rectángulos
# Bolitas
bolitas_box = (1421, 39, 1421 + 415, 39 + 75)  # (x1, y1, x2, y2) 
# Botones
botones_box = (1470, 958, 1470 + 377 + 5, 958 + 109)  # (x1, y1, x2, y2)

# Función para cubrir áreas específicas de la imagen
def cover_elements(image_path):
    try:
        with Image.open(image_path) as img:
            draw = ImageDraw.Draw(img)
            
            # Cubrir botones
            draw.rectangle(botones_box, fill="white")
            
            # Cubrir las bolitas de progreso
            draw.rectangle(bolitas_box, fill="white")
            
            # Guardar los cambios sobrescribiendo la imagen original
            img.save(image_path)
            print(f"Procesado: {image_path}")
    except Exception as e:
        print(f"Error procesando {image_path}: {e}")

# Recorrer las carpetas y procesar las imágenes
for folder in folders:
    folder_path = os.path.join(base_dir, folder)
    if os.path.exists(folder_path):
        for filename in os.listdir(folder_path):
            if filename.lower().endswith(('.png', '.jpg', '.jpeg')):
                image_path = os.path.join(folder_path, filename)
                cover_elements(image_path)
    else:
        print(f"Carpeta no encontrada: {folder_path}")
