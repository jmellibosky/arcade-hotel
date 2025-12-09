Tópicos por cada cliente con el formato `{tipo}/{id}/{accion}`
Donde:
- `tipo` es `req` o `res` según si el mensaje es una solicitud al cliente o una respuesta de este.
- `id` es un identificador único de cada arcade o expendedora, prefijados con `a` y `e`, respectivamente.
- `accion` es el comando

| cliente | `accion`         |
| ------- | ---------------- |
| arcade  | `coin`           |
| arcade  | `config-default` |
| arcade  | `status`         |
#### `coin`
Request:
```
coin{
	id: 1/1, <<< identificador único de solicitud
	key: "Coin 1", <<< nombre del campo a ejecutar (cambia según juego y jugador)
}
```
Response:
```
{
	id: 1/1, <<< identificador único de solicitud
	result: true, <<< resultado de la operación (false si ocurrió algún error)
	message: "", <<< mensaje de error (vacío si no hay error)
}
```

#### `default`
Request:
```
{
	type: "default",
	content: {
		id: 1/2, <<< identificador único de solicitud
		file: "ssriderseaa.rar", <<< nombre del archivo que debe abrir la próxima vez
		restart: true, <<< indica que debe reiniciarse 
	}
}
```
Response:
```
{
	id: 1/2, <<< identificador único de solicitud
	result: true, <<< resultado de la operación (false si ocurrió algún error)
	message: "", <<< mensaje de error (vacío si no hay error)
}
```

#### `status`
Request:
```
{
	id: 1/3, <<< identificador único de solicitud
}
```
Response:
```
{
	id: 1/3, <<< identificador único de solicitud
	version: "1.0.0", <<< versión del programa puente
}
```

### Tópicos y mensajes:
- `req/aXX/coin`: otorgar crédito a un juego
	- `tecla`: tecla para otorgar crédito en el juego según jugador elegido
- `req/aXX/config-default`: cambiar la configuración inicial de mame
	- `archivo`: nombre del archivo a utilizar como configuración inicial de mame
- `req/aXX/status`: ping
- `req/eXX/coin`: otorga un crédito a una expendedora
	- `bahia`: canaleta que debe dispensar la expendedora
- `req/eXX/sensor`: solicita y brinda información sobre la temperatura y humedad de la expendedora
	- `{temperatura}/{humedad}`
- `req/eXX/status`: ping
	- `{ "reqId": "123" }`
