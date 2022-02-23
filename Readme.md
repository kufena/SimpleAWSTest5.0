## Simple AWS Test of API and Parameters ##

The test here is several-fold:

    Firstly, testing CodeBuild, CodeDeploy and CodePipeline from a github repo.
    Secondly, using Parameter Store to configure the simple application.

I guess this'll be deployed eventually to an EC2 instance.  It only needs to be simple.

You'll need a couple of things for CodeBuild local.  Download the standard image sources here:

    git clone https://github.com/aws/aws-codebuild-docker-images.git

and then build the latest one (the standard 5.0 has .net 5.0 but not .net 6.0 yet.)

    sudo docker build -t aws/codebuild/standard:5.0 .

I used sudo here as it wouldn't build if I didn't.  It takes an absolute age to build though.

Then you'll need the local image too:

    docker pull amazon/aws-codebuild-local:latest

Use something like

    ./codebuild_build.sh -i aws/codebuild/standard:5.0 -a ~/work/artefacts -s .

to use the AWS CodeBuild locally.  Here, -i says which codebuild standard image to use - 5.0 does .net 5.0 but not 6.0.  -a says where to put the artefacts created. -s is the source.
