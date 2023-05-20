FROM php:7.2-apache

# Kopiowanie
COPY rest_api/ /var/www/html/

RUN apt-get update
RUN apt-get install -y apache2
RUN a2enmod rewrite
RUN docker-php-ext-install mysqli