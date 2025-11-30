-- auto_coin_file.lua
local ioport = manager.machine.ioport
local COMMAND_FILE = "C:/Users/musi/Desktop/Nueva carpeta (2)/Scripts/mame_cmd.txt"

-- Buscar un campo por nombre exacto (case-insensitive)
local function find_field_by_name(name)
    name = string.lower(name)
    for _, port in pairs(ioport.ports) do
        if port and port.fields then
            for fname, field in pairs(port.fields) do
                if string.lower(fname) == name then
                    return field
                end
            end
        end
    end
    return nil
end

-- Pulsar un field
local function pulse_field(field)
    if not field then return end
    field:set_value(1)
    emu.wait(0.05)
    field:set_value(0)
end

-- Leer archivo de comandos
local function read_command()
    local f = io.open(COMMAND_FILE, "r")
    if f then
        local content = f:read("*l")
        f:close()
        if content and #content > 0 then
            return content
        end
    end
    return nil
end

-- Borrar archivo después de usarlo
local function clear_command_file()
    local f = io.open(COMMAND_FILE, "w")
    if f then
        f:write("")
        f:close()
    end
end

-- Loop principal
emu.register_frame(function()
    local cmd = read_command()
    if cmd then
        print("[auto_coin_file] Comando recibido: " .. cmd)
        local field = find_field_by_name(cmd)
        if field then
            print("[auto_coin_file] Pulsando field: " .. cmd)
            pulse_field(field)
        else
            print("[auto_coin_file] No se encontró field: " .. cmd)
        end
        clear_command_file()
    end
end)
