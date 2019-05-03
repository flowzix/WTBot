# WTBot - Supreme shopping bot

## What is it?
Supreme shopping automation bot that uses programatically-generated requests to buy items quickly.

## Why was it better than Chrome extensions?
It was better than Chrome extensions, because it didn't need to render the webpage to access its functionalities. It's based on requests, and it's not interpreting the HTML code(nothing else than a simple RegEx).

## Is it working?
No, this bot is currently not working. However, the request part stayed the same over time, so it can be a great base for creating a new one.

## How fast can it be?
Like really fast. With a proper connection you can checkout in ca. 1.5 seconds.

## Why isn't it working anymore?
* With "new" reCaptcha, Google has introduced variable called challenge_ts, which informs you about the time, at which reCaptcha was solved. This kills most of reCaptcha harvesters, because website owner can verify, whether the reCaptcha was solved before it appeared on the website or not.
* Recently, Supreme has introduced a special javascript source, which developers call "pooky.js". This script is being placed at the website ca. 2 minutes before drop. It's fully obfuscated and contains about 65,000 lines being "prettified". It's based on key exchange protocols and it generates special requests bodies, which enables website for further verification.

## What can I do to make this work?
* reCaptcha - you need to fake the browser and use google accounts. You don't get reCaptcha when you are logged to your google account, and you're doing everything manually. You can do this by specyfing proper user-agent and thinking about other data that your browser may provide to reCaptcha. You can also use cookies from your browser, however this is not the best solution(it created dependencies).
* pooky.js - This one might be more difficult. You need to deobfuscate the code and understand the way it works, and generate the requests programatically. The other idea I came up is emulating js in C#. You can take the sourcecode of pooky.js and try to simulate it's behaviour as black-box, however this is just my, maybe not accomplishable.

## Has somebody "defeated" those mechanisms?
Yes, definitly, look at the ~3.5s sellout times. It's impossible to achieve them in browser or manually. The most famous bot is CyberAIO.

## Further about this project
It's not something to learn from. I am not a C# developer, therefore you should rather treat this project as a bundle of ideas.
