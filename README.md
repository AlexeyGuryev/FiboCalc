# Межпроцессный расчет чисел Фибоначчи

Расчет чисел Фибоначчи в двух процессах, обменивающихся результатами расчетов с помощью Rest-вызовов и шины на MassTransit

Для запуска расчета необходимо:
1. собрать и запустить в IIS/IISExpress вебприложение ApiProcessor
2. собрать и запустить в консоли приложение BusProcessor, указав в первом параметре число одновременных расчетов: .\BusProcessor.exe 10
