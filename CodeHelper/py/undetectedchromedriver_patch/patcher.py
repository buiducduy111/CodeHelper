import os
import io
import re

unpatchedDir = 'unpatched'

def patchExe(driverFileName):
    fullPatchDriverFile = unpatchedDir+'\\'+driverFileName
    print('Patching: '+fullPatchDriverFile + "----------------------------------")
    with io.open(fullPatchDriverFile, "r+b") as fh:
        content = fh.read()
        # match_injected_codeblock = re.search(rb"{window.*;}", content)
        match_injected_codeblock = re.search(rb"\{window\.cdc.*?;\}", content)
        if match_injected_codeblock:
            target_bytes = match_injected_codeblock[0]
            new_target_bytes = (
                b'{console.log("undetected chromedriver 1337!")}'.ljust(
                    len(target_bytes), b" "
                )
            )
            new_content = content.replace(target_bytes, new_target_bytes)
            if new_content == content:
                print("something went wrong patching the driver binary. could not find injection code block")
            else:
                print("found block:\n%s\nreplacing with:\n%s"% (target_bytes, new_target_bytes))
            fh.seek(0)
            fh.write(new_content)

all_files = os.listdir(unpatchedDir)
file_list = [f for f in all_files if os.path.isfile(os.path.join(unpatchedDir, f))]

for file in file_list:
    patchExe(file)



