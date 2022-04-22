# This is a placeholder until the funda-specific Docker image can be integrated
FROM mcr.microsoft.com/dotnet/sdk:6.0
ENV ASPNETCORE_ENVIRONMENT=production

ENV TZ=Europe/Amsterdam
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Install DataDog APM tracer
RUN curl -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v2.5.1/datadog-dotnet-apm_2.5.1_amd64.deb \
  && dpkg -i ./datadog-dotnet-apm_2.5.1_amd64.deb \
  && chmod 755 /opt/datadog/createLogPath.sh \
  && ./opt/datadog/createLogPath.sh

WORKDIR /app
COPY publish .

# TODO: Is below still necessary?
# # Sets security level to 1 (default is 2 and does not allow self signed 
# # SSL certificates on machine level). See https://github.com/dotnet/corefx/issues/40538
# RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed \
#  && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf

RUN adduser --disabled-password --gecos "" appuser --force-badname --home /home/appuser --uid 5000 \
  && chown -R appuser /app

USER appuser

EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "${{ values.namespacePrefix }}.dll"]
