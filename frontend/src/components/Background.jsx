import { useState, useEffect } from "react";

export default function Background() {
    const images = [
        'assets/bg/arcade1.png',
        'assets/bg/arcade2.png',
        'assets/bg/arcade3.jpg',
        'assets/bg/arcade4.jpg',
        'assets/bg/arcade5.jpg'
    ];

    const [index, setIndex] = useState(0);

    useEffect(() => {
        const interval = setInterval(() => {
            setIndex((prevIndex) => (prevIndex + 1) % images.length);
        }, 10000);

        return () => clearInterval(interval);
    }, [images.length]);

    return (
        <div className="background">
            {
                images.map((o, i) => (
                    <img key={i} src={o} alt={"Fondo " + i} className={index === i ? "active" : ""} />
                ))
            }
        </div>
    )
}