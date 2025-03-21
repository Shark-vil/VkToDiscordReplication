# Репликация постов из ВК (VK) в Дискорд (Discord)

## Демонстрация

### Пост в VK

![VK](https://i.imgur.com/10sNlzb.png)

### Репликация в Discord

![Discord](https://i.imgur.com/Uxenbuf.png)

## Для работы требуется NET 8

Пример установки на Debian 12

```bash
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```

```bash
sudo apt update
sudo apt install -y aspnetcore-runtime-8.0
```

## Как использовать?

1. Загрузите файл "**VkToDiscordReplication**" на сервер
2. Выдайте разрешение на запуск "**chmod +x VkToDiscordReplication**"
3. Запустите один раз через "**./VkToDiscordReplication**"
4. После первого запуска выдаст ошибку, это нормально. Рядом с исполняемым файлом появится файл конфигурации "**config.json**", отеройте его и отредактируйте.
5. После изменения файла "**config.json**" вы можете повторно запустить программу

## Как запускать в фоне?

Вы можете использовать "**tmux**":

```bash
apt update
apt install -y tmux
```

Создайте скрипт с примерно таким содержимым

*start.sh*
```bash
tmux new-session -d -s vktodiscord ./VkToDiscordReplication
```

## Структура конфига

```json
{
    // Список сообществ
    "groups": [
        {
            // Токен доступа к сообществу с доступом "управление сообществом" и "стена сообщества"
            "access_token": "vk1.a.SFZNCNA...",
            // Версия LongPoll. Рекомендуется 5.150 и выше
            "longpoll_version": "5.150",
            // Идентификатор сообщества
            "group_id": "000000000",
            // Просто не трогайте
            "lp_version": "3",
            // Просто не трогайте
            "need_pts": "0",
            // Сссылка на вебхук чата на вашем канале в Discord
            "discord_webhook": "https://discord.com/api/webhooks/1351754328577496/SHHVBAS...",
            // Цвет подсветки Embed. Можно оставить пустым, если хотите использовать стандратный
            "embed_color": "#29a349",
            // Произвольное имя сообщества
            "custom_name": "",
            // Произвольный аватар сообщества
            "custom_avatar": ""
        }
    ]
}
```