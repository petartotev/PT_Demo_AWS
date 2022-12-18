# PT_Demo_AwsSqsQueueAndLambda

## Content

- [General Information](#general-information)
- [Architecture](#architecture)
- [Setup](#setup)
  - [AWS SQS Queue Setup](#aws-sqs-queue-setup)
  - [Web API Setup](#web-api-setup)
  - [AWS Lambda Setup](#aws-lambda-setup)

---

<a name="general-information" ></a>

## General Information

PT_Demo_AwsSqsQueueAndLambda is a demo project to test both Lambdas and SQS Queues.

---

<a name="architecture" ></a>

## Architecture

![Workflow](res/images/workflow.jpg)

---

<a name="setup" ></a>

## Setup

<a name="aws-sqs-queue-setup" ></a>

### AWS SQS Queue Setup

In the aws.amazon.com UI, create a new SQS Queue.  
With the UI, one can send a message.  
Then, from a CMD console, you can get the message using the following AWS command:  

```
aws sqs receive-message --queue-url https://sqs.eu-central-1.amazonaws.com/{id}/{queue-name}
````

Copy the URL of the SQS Queue and place it as a 'AwsSqsQueueUrl' variable in the Web API's appsettings.json.

---

<a name="web-api-setup" ></a>

### Web API Setup

Run AwsSqsQueueProducer.API locally on port 5194: http://localhost:5194 .

Having the SQS Queue URL in the **appsettings.json**, all incoming HTTP Post requests to http://localhost:5194/api/articles should be processed and resent through an **AmazonSQSClient** (coming from NuGet package **Amazon.SQS**) to the SQS Queue.

The format of the body of the Post request should be as follows:
```json
{
    "title": "title",
    "description": "description",
    "content": "content",
    "dateCreated": "2022-11-07",
    "authors": [
        {
            "firstName": "firstName1",
            "lastName": "lastName1",
            "age": 33
        },
        {
            "firstName": "firstName2",
            "lastName": "lastName2",
            "age": 22
        }
    ],
    "publishers": [
        {
            "name": "name",
            "datePublished": "2022-11-24"
        }
    ]
}
```
You should now be able to find all sent requests to Web API in the SQS Queue.  
You can get such messages from the queue using the aws command mentioned in section [AWS SQS Queue Setup](#aws-sqs-queue-setup).

---

<a name="aws-lambda-setup" ></a>

#### AWS Lambda Setup

1. First, you need to configure your profile:

```
aws configure --profile {profile}
    AWS Access Key ID [********************]: {here}
	AWS Secret Access Key [********************]: {here}
	Default region name [eu-central-1]: eu-central-1
	Default output format [None]:
```

2. If you don't have the Amazon.Lambda.Tools installed, you need to execute the following command:

```
dotnet tool install -g Amazon.Lambda.Tools --version 5.4.1 --add-source https://api.nuget.org/v3/index.json
```

3. Next, you need to have the Amazon Lambda Templates installed:

```
dotnet new --install Amazon.Lambda.Templates::6.2.0
```

4. Then you can create your own Lambda project:

```
dotnet new lambda.EmptyFunction --name {project-name}
```

5. Implement the project by receiving a SQSEvent, processing it and sending its content to a personal email using an **AmazonSimpleEmailServiceClient** (coming from NuGet package **Amazon.SimpleEmail**).

6. If needed, configure again?

```
aws configure
    AWS Access Key ID [********************]: {here}
	AWS Secret Access Key [********************]: {here}
	Default region name [eu-central-1]: eu-central-1
	Default output format [None]:
```

7. Deploy the project in AWS:

```
dotnet lambda deploy-function {project-name} --add-source https://api.nuget.org/v3/index.json
    {enter role choice}
```

8. Go in the AWS UI, find your newly created Lambda and add a new Trigger of type 'SQS Trigger' and choose the SQS Queue name created in section  [AWS SQS Queue Setup](#aws-sqs-queue-setup).

9. Now every Post request you send to Web API should be processed and sent as a message to an AWS SQS Queue. This message should be processed by the AWS Lambda and sent via an email client to your personal email.  
That's it.