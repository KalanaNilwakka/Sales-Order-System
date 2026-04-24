'use client'

interface WindowFrameProps {
  title: string
  children: React.ReactNode
}

export default function WindowFrame({ title, children }: WindowFrameProps) {
  return (
    <div className="min-h-screen bg-gray-200 p-4">
      <div className="bg-white border border-gray-400 shadow-md">
        {/* Title Bar */}
        <div className="bg-gray-300 border-b border-gray-400 px-2 py-1 flex items-center">
          <div className="flex gap-1">
            <div className="w-3 h-3 rounded-full bg-green-500 border border-green-600 flex items-center justify-center text-[8px] text-green-800">+</div>
            <div className="w-3 h-3 rounded-full bg-yellow-500 border border-yellow-600 flex items-center justify-center text-[8px] text-yellow-800">-</div>
            <div className="w-3 h-3 rounded-full bg-red-500 border border-red-600 flex items-center justify-center text-[8px] text-red-800">×</div>
          </div>
          <div className="flex-1 text-center text-sm font-medium text-gray-800">{title}</div>
        </div>
        {/* Content */}
        <div className="p-4">
          {children}
        </div>
      </div>
    </div>
  )
}
