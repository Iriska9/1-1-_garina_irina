import os
import json
import xml.etree.ElementTree as ET
import zipfile
import psutil

# ==========================================
# 1. Вывод информации о дисках
# ==========================================
def get_disk_info():
    print("Информация о логических дисках:")
    partitions = psutil.disk_partitions()
    for partition in partitions:
        try:
            usage = psutil.disk_usage(partition.mountpoint)
            print(f"Диск: {partition.device}")
            print(f"Точка монтирования: {partition.mountpoint}")
            print(f"Файловая система: {partition.fstype}")
            print(f"Размер: {usage.total // (1024 ** 3)} GB")
            print(f"Свободно: {usage.free // (1024 ** 3)} GB\n")
        except PermissionError:
            print(f"Нет доступа к диску: {partition.device}\n")

# ==========================================
# 2. Работа с файлами (создание, запись, чтение, удаление)
# ==========================================
def create_file(file_name):
    with open(file_name, 'w') as file:
        content = input("Введите строку для записи в файл: ")
        file.write(content)
    print(f"Файл '{file_name}' создан и запись завершена.")

def read_file(file_name):
    if os.path.exists(file_name):
        with open(file_name, 'r') as file:
            content = file.read()
        print(f"Содержимое файла '{file_name}':\n{content}")
    else:
        print(f"Файл '{file_name}' не найден.")

def delete_file(file_name):
    if os.path.exists(file_name):
        os.remove(file_name)
        print(f"Файл '{file_name}' удален.")
    else:
        print(f"Файл '{file_name}' не существует.")

# ==========================================
# 3. Работа с форматом JSON (создание, чтение, удаление)
# ==========================================
def create_json_file(file_name):
    data = {}
    print("Введите данные для JSON файла (пустая строка завершает ввод):")
    while True:
        key = input("Ключ: ")
        if key == "":
            break
        value = input("Значение: ")
        data[key] = value

    with open(file_name, 'w') as json_file:
        json.dump(data, json_file, indent=4)
    print(f"JSON файл '{file_name}' создан.")

def read_json_file(file_name):
    if os.path.exists(file_name):
        with open(file_name, 'r') as json_file:
            data = json.load(json_file)
        print(f"Содержимое JSON файла '{file_name}':\n{json.dumps(data, indent=4)}")
    else:
        print(f"JSON файл '{file_name}' не найден.")

def delete_json_file(file_name):
    delete_file(file_name)

# ==========================================
# 4. Работа с форматом XML (создание, запись, чтение, удаление)
# ==========================================
def create_xml_file(file_name):
    root = ET.Element("root")

    print("Введите данные для XML файла (пустая строка завершает ввод):")
    while True:
        tag = input("Тег: ")
        if tag == "":
            break
        text = input("Текст: ")
        child = ET.Element(tag)
        child.text = text
        root.append(child)

    tree = ET.ElementTree(root)
    tree.write(file_name)
    print(f"XML файл '{file_name}' создан.")

def read_xml_file(file_name):
    if os.path.exists(file_name):
        tree = ET.parse(file_name)
        root = tree.getroot()
        print(f"Содержимое XML файла '{file_name}':")
        for elem in root:
            print(f"{elem.tag}: {elem.text}")
    else:
        print(f"XML файл '{file_name}' не найден.")

def delete_xml_file(file_name):
    delete_file(file_name)

# ==========================================
# 5. Работа с ZIP архивами (создание, добавление файла, извлечение)
# ==========================================
def create_zip_archive(zip_name, file_name):
    with zipfile.ZipFile(zip_name, 'w') as zipf:
        zipf.write(file_name)
    print(f"Файл '{file_name}' добавлен в архив '{zip_name}'.")

def extract_zip_archive(zip_name, extract_path="./"):
    with zipfile.ZipFile(zip_name, 'r') as zipf:
        zipf.extractall(extract_path)
    print(f"Архив '{zip_name}' распакован в папку '{extract_path}'.")

def delete_zip_archive(zip_name):
    delete_file(zip_name)

# ==========================================
# Основная программа
# ==========================================
if __name__ == "__main__":
    while True:
        print("\nМеню:")
        print("1. Вывести информацию о дисках")
        print("2. Работа с файлами")
        print("3. Работа с JSON файлами")
        print("4. Работа с XML файлами")
        print("5. Работа с ZIP архивами")
        print("0. Выйти")
        choice = input("Выберите действие: ")

        if choice == "1":
            get_disk_info()
        elif choice == "2":
            print("\n1. Создать файл")
            print("2. Прочитать файл")
            print("3. Удалить файл")
            file_choice = input("Выберите действие: ")
            file_name = input("Введите имя файла: ")

            if file_choice == "1":
                create_file(file_name)
            elif file_choice == "2":
                read_file(file_name)
            elif file_choice == "3":
                delete_file(file_name)
        elif choice == "3":
            print("\n1. Создать JSON файл")
            print("2. Прочитать JSON файл")
            print("3. Удалить JSON файл")
            json_choice = input("Выберите действие: ")
            file_name = input("Введите имя JSON файла: ")

            if json_choice == "1":
                create_json_file(file_name)
            elif json_choice == "2":
                read_json_file(file_name)
            elif json_choice == "3":
                delete_json_file(file_name)
        elif choice == "4":
            print("\n1. Создать XML файл")
            print("2. Прочитать XML файл")
            print("3. Удалить XML файл")
            xml_choice = input("Выберите действие: ")
            file_name = input("Введите имя XML файла: ")

            if xml_choice == "1":
                create_xml_file(file_name)
            elif xml_choice == "2":
                read_xml_file(file_name)
            elif xml_choice == "3":
                delete_xml_file(file_name)
        elif choice == "5":
            print("\n1. Создать ZIP архив и добавить файл")
            print("2. Извлечь архив")
            print("3. Удалить архив")
            zip_choice = input("Выберите действие: ")
            zip_name = input("Введите имя ZIP архива: ")

            if zip_choice == "1":
                file_name = input("Введите имя файла для добавления в архив: ")
                create_zip_archive(zip_name, file_name)
            elif zip_choice == "2":
                extract_zip_archive(zip_name)
            elif zip_choice == "3":
                delete_zip_archive(zip_name)
        elif choice == "0":
            print("Выход из программы.")
            break
        else:
            print("Неверный выбор. Попробуйте снова.")
