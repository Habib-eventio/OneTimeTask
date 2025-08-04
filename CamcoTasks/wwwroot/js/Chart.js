window.renderSpentChart = function (budgetItems, totalSpent) {
    console.log("Initializing chart...");

    const chartDom = document.getElementById('spentChart');
    if (!chartDom) {
        console.warn('Chart container not found!');
        return;
    }

    const myChart = echarts.init(chartDom);

    const categoryIcons = {
        "Invitations & Stationary": 'circle',
        "Decor & Design": 'rect',
        "Food & Beverage": 'triangle'
    };

    // Ensure correct casing for JS property access (camelCase from C# objects)
    const data = (budgetItems || []).map(item => ({
        value: item.actualAmount,
        name: item.categoryName,
        itemStyle: { color: item.color }, // ✅ Ensure slice color matches status
        color: item.color, // ✅ Directly assign color for pie slice
        icon: categoryIcons[item.categoryName] || 'circle'
    }));
    const hasData = data.length > 0;

    function getCenterText(amount) {
        return [
            '{spent|TASKS IN PROGRESS}',
            `${amount.toLocaleString(undefined, {
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            })}`
        ].join('\n');
    }

    const textPos = { left: 'center', top: 'center' };
    const seriesPos = {
        center: ['50%', '50%'],
        radius: hasData ? ['40%', '75%'] : ['50%', '80%']
    };

    const options = {
        tooltip: {
            trigger: 'item',
            backgroundColor: '#F5F5F4',
            borderColor: '#ECEBE9',
            borderWidth: 1,
            borderRadius: 4,
            padding: [2, 6, 2, 6],
            textStyle: { color: '#000' },
            formatter: function (params) {
                return `
                    <div style="text-align:left;font-weight:500;font-size:12px;line-height:16px;margin-bottom:6px;">
                        ${params.name}
                    </div>
                    <div style="text-align:left;font-size:12px;line-height:16px;">
                        ${params.value}
                    </div>`;
            }
        },
        legend: {
            show: hasData,
            orient: 'horizontal',
            left: 'center',
            bottom: 0,
            itemWidth: 16,
            itemHeight: 16,
            textStyle: {
                fontSize: 12,
                color: '#2E2E2E',
                rich: {
                    text: {
                        align: 'right',
                        verticalAlign: 'middle',
                        fontSize: 12,
                        fontWeight: '600',
                        color: '#44403C',
                        fontFamily: 'Roboto, sans-serif',
                        padding: [0, 0, 0, 8]
                    }
                }
            },
            formatter: name => `{text|${name}}`,
            data: data.map(item => ({
                name: item.name,
                icon: item.icon
            }))
        },
        graphic: {
            elements: [
                {
                    type: 'text',
                    left: textPos.left,
                    top: textPos.top,
                    style: {
                        text: getCenterText(totalSpent),
                        textAlign: 'center',
                        textVerticalAlign: 'middle',
                        fontSize: 24,
                        fontWeight: 500,
                        lineHeight: 32,
                        fill: '#1A1A1A',
                        fontFamily: 'Roboto',
                        rich: {
                            spent: {
                                fontFamily: 'Roboto',
                                fontWeight: 500,
                                fontSize: 12,
                                lineHeight: 16,
                                letterSpacing: '4%',
                                textAlign: 'center',
                                textTransform: 'uppercase',
                                fill: '#44403C'
                            },
                            amount: {
                                fontFamily: 'Roboto',
                                fontWeight: 500,
                                fontSize: 24,
                                lineHeight: 32,
                                fill: '#171412'
                            }
                        }
                    }
                }
            ]
        },
        series: [
            {
                name: 'Spent',
                type: 'pie',
                radius: seriesPos.radius,
                center: seriesPos.center,
                avoidLabelOverlap: false,
                itemStyle: {
                    borderRadius: 4,
                    borderColor: '#fff',
                    borderWidth: 2
                },
                label: { show: false },
                data: hasData ? data : [],
                animation: true,
                animationDuration: 300
            }
        ]
    };

    myChart.setOption(options, true);

    function debounce(func, wait) {
        let timeout;
        return function () {
            clearTimeout(timeout);
            timeout = setTimeout(func, wait);
        };
    }

    const debouncedResize = debounce(() => myChart.resize(), 200);
    window.addEventListener('resize', debouncedResize);

    myChart.on('legendselectchanged', function () {
        myChart.setOption({
            graphic: {
                elements: [
                    {
                        type: 'text',
                        left: textPos.left,
                        top: textPos.top,
                        style: {
                            text: getCenterText(totalSpent),
                            textAlign: 'center',
                            textVerticalAlign: 'middle',
                            fontSize: 24,
                            fontWeight: 500,
                            lineHeight: 32,
                            fill: '#1A1A1A',
                            fontFamily: 'Roboto'
                        }
                    }
                ]
            }
        });
    });

    return function () {
        window.removeEventListener('resize', debouncedResize);
        myChart.dispose();
    };
};
