Este servicio de Windows, obtiene las cadenas en base64 de los registros de una tabla de BD de postgres, convierte a formato de imagen y reemplaza la cadena base64 por la ruta en la que se inserta la imagen.

Para la ejecución de este servicio, debe haber una base de datos, con tabla y registro donde obtengan las cadenas en base64.

Para antes de correr este servicio, debe crear una base de datos llamada Imagenes y en su query tool ejecutar Query.sql en un gestor postgres

Para crear el servicio como servicio de windows, se debe realizar una publicación del proyecto y ejecutar el siguiente comando en Windows PowerShell:

New-Service -Name "serviciobase64tofile" -BinaryPathName "C:\Ruta\De\La\Publicacion\Base64ToFileService.exe" -Description "Este servicio convierte a archivo la base64 obtenida de un registro, manda a insertar la imagen en carpeta y reemplaza la cadena base64 por la ruta en la que guardó el archivo."


--Adicionales

sc.exe delete "serviciobase64tofile" --Elimina el servicio

sc.exe stop "serviciobase64tofile" --Detiene el servicio

Hecho por Janier Urbina