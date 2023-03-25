# Тестовое задание
### Description
Given a quite large tree describing UI elements with size and position inside parent (child elements are always contained in parent) and current viewport (that it, (x, y, width, height)) implement fast visibility tester quickly finding partially or fully visible elements in the viewport. Algorithm must be optimized for cases like scrolling (in both axis) when fixed size viewport slides over UI elements.

Concurrent algorithm would be a plus

### Реализация
#### VisibilityChecker
Библиотека, основная сущность которой - UIMonitor, содержащий в себе UIElements и Viewport. 
- UIElement - прямоугольник с координатами сторон и массивом ссылок на внутренние прямоугольники.
- Viewport - движущееся прямоугольное окно на экране.

Методы ScrollHorizontally и ScrollVertically двигают Viewport по экрану.

Методы TestVisibility и TestVisibilityConcurrent рекурсивно проходят по элементам дерева (прямоугольникам), вычисляя по осям X и Y, виден ли данный прямоугольник.
В случае, если он полностью виден или полностью не виден, рекурсия в глубь не идет, так как все его внутренние элементы так же полностью видны/не видны.
Если он виден частично, то по внутренним прямоугольникам так же проходим, так как для них ответ может быть любой. 
Параллельная версия алгоритма запускает параллельно рекурсивный обход всех верхних прямоугольников. Была попытка сделать параллельные запуски внутри рекурсии, но тратилось слишком много времени на само распараллеливание.

В качестве оптимизации был вариант пересчитывать видимость только вдоль одной из координат при скроллинге вдоль одной оси, 
но было решено от этого отказаться, так как для этого пришлось бы хранить 2 массива ответов для осей X и Y, а выигрышь в скорости был небольшой, 
так как проверка, виден ли прямоугольник, требует только несколько операций сравнения.
Была идея делать бинпоиск по координатам, предварительно отсортировав координаты проекций прямоугольников, но непонятно, как быстро пересекать 2 полученных множества ответов по координатам

Метод RecalculateVisibilityResult возвращает ответ в виде 3 массивов - видимые, невидимые и частично видимые элементы.

#### ConsoleVisibilityChecker
Консольное приложение, на вход которому дается файл с перечислением координат прямоугольников и их предков, а так же начальные координаты Viewport.

ConsolePrinter позволяет печатать ответ в 2 вариациях:
- краткий, со списком всех частично видимых элементов, а также списком верхних полностью видимых или полностью невидимых элементов
- полный, со списком всех частично видимых элементов, а также списком всех полностью видимых элементов

Поддерживаемые команды:
- v - краткий список элементов
- fv - полный список элементов
- hor x - сдвинуть Viewport на x вправо
- ver x - сдвинуть Viewport на x вверх
- m - вывести в консоль все элементы UIMonitor (координаты всех прямоугольников и положение Viewport)
- h - вывести список команд
- e - выход

#### Tests
Unit тесты. Реализованны пара простых тестов с маленьким количеством элементов, пара тестов с вызовом Scroll, один тест с большим входным файлом