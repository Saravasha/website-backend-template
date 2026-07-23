document.addEventListener("DOMContentLoaded", async () => {


    const containers =
        document.querySelectorAll(".asset-render");

    for (const container of containers) {

        const assetId =
            container.dataset.assetId;

        if (!assetId)
            continue;

        try {

            const response =
                await fetch(`/Asset/Render/${assetId}`);

            if (!response.ok)
                throw new Error();

            const html =
                await response.text();

            container.innerHTML = html;

            document.querySelectorAll(".asset-render img").forEach(img => {
                img.addEventListener("error", () => img.remove());
            });
        }
        catch {

            container.innerHTML =
                "<p>Unable to load document.</p>";
        }
    }
});