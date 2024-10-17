from fastapi import FastAPI
import mysql.connector

app = FastAPI(docs_url='/InfoComunicador/docs', redoc_url=None)

# Función para leer los parámetros de conexión desde el archivo "conf.txt"
def read_db_config(filename):
    db_config = {}
    with open(filename) as f:
        for line in f:
            key, value = line.strip().split('=')
            db_config[key] = value
    return db_config

@app.get("/InfoComunicador/")
def bienvenida():
	return {"Hola":"Mundo"}

@app.get("/InfoComunicador/color/")
async def leer_color():
    try:
        config_file = "conf.txt"
        db_config = read_db_config(config_file)

        connection = mysql.connector.connect(**db_config)

        if connection.is_connected():
            cursor = connection.cursor()

            sp_name = 'InfoComunicador.getColor'
            Color = None

            Color = cursor.callproc(sp_name, [Color])[0]
            Color = {"Color":Color}

            cursor.close()
            connection.close()
    except mysql.connector.Error as error:
        Color = {"Color":"rgb(255,255,255)","Error": "Fallo al conectarse a la base de datos: {}".format(error)}
    return Color

@app.get("/InfoComunicador/cartel/")
async def leer_cartel():
    try:
        config_file = "conf.txt"
        db_config = read_db_config(config_file)

        connection = mysql.connector.connect(**db_config)

        if connection.is_connected():
            cursor = connection.cursor()

            sp_name = 'InfoComunicador.getUltimoCartel'
            Cartel = ""

            Cartel = cursor.callproc(sp_name, [Cartel])[0]
            Cartel = {"Cartel":Cartel}

            cursor.close()
            connection.close()
    except mysql.connector.Error as error:
        Cartel = {"Cartel":"Fallo al conectarse a la base de datos: {}".format(error)}
    return Cartel

@app.post("/InfoComunicador/cartel/nuevo")
async def nuevo_cartel(Cartel: str):
    try:
        config_file = "conf.txt"
        db_config = read_db_config(config_file)

        connection = mysql.connector.connect(**db_config)

        if connection.is_connected():
            cursor = connection.cursor()

            sp_name = 'InfoComunicador.insertCartel'

            cursor.callproc(sp_name, [Cartel])

            Result = {"Estado":"Se insertó el nuevo cartel con éxito."}

            cursor.close()
            connection.close()
    except mysql.connector.Error as error:
        Result = {"Cartel":"Fallo al conectarse a la base de datos: {}".format(error)}
    return Result

@app.put("/InfoComunicador/color/actualizar")
async def actualizar_color(Color: str):
    try:
        config_file = "conf.txt"
        db_config = read_db_config(config_file)

        connection = mysql.connector.connect(**db_config)

        if connection.is_connected():
            cursor = connection.cursor()

            sp_name = 'InfoComunicador.putColor'

            cursor.callproc(sp_name, [Color])
            Result = {"Estado":"Se actualizó el color con éxito."}

            cursor.close()
            connection.close()
    except mysql.connector.Error as error:
        Result = {"Estado": "Error: fallo al conectarse a la base de datos: {}".format(error)}
    return Result